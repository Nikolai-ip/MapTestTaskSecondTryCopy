using System;
using _Game.Source.Application.Validation;
using _Game.Source.Data.StaticData;
using _Game.Source.Domain;
using _Game.Source.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace _Game.Source.Application.Factories.PinFactories
{
    public class RunTimePinCreator : IInitializable, IDisposable
    {
        private readonly IInputService _inputService;
        private readonly PinFactory _pinFactory;
        private readonly IValidator<PinCanBePlacedContext> _createPinValidator;
        private readonly DefaultPinData_SO _defaultPinData;
        private readonly IRepository<Pin> _pinRepository;

        public RunTimePinCreator(IInputService inputService, PinFactory pinFactory, 
            IValidator<PinCanBePlacedContext> createPinValidator, DefaultPinData_SO defaultPinData, IRepository<Pin> pinRepository)
        {
            _inputService = inputService;
            _pinFactory = pinFactory;
            _createPinValidator = createPinValidator;
            _defaultPinData = defaultPinData;
            _pinRepository = pinRepository;
        }

        public void Initialize()
        {
            _inputService.OnPointerUp += TryCreatePin;
        }

        private void TryCreatePin(Vector2 screenMousePos)
        {
            if (_createPinValidator.Validate(new PinCanBePlacedContext(screenMousePos)))
            {
                var pin = new Pin() { 
                        Id = Guid.NewGuid(), 
                        Position = screenMousePos,
                        Name = _defaultPinData.Name, 
                        Description = _defaultPinData.Description,
                        Image = String.Empty
                };
                _pinFactory.Create(() =>
                {
                    _pinRepository.Add(pin);
                    return pin;
                });
;
            }

        }
        public void Dispose()
        {
            _inputService.OnPointerUp -= TryCreatePin;
        }
    }
}