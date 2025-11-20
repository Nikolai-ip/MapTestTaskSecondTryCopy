using _Game.Source.Domain;
using UnityEngine;

namespace _Game.Source.Application.PinUseCases
{
    public class PinDisabler: IPinRemover, IPinSetterVisitor
    {
        private readonly GameObject _pinRoot;
        private readonly PinComponent _pinComponent;
        private readonly IRepository<Pin> _pinRepository;
        private Pin _pin;

        public PinDisabler(GameObject pinRoot, PinComponent pinComponent, IRepository<Pin> pinRepository)
        {
            _pinRoot = pinRoot;
            _pinComponent = pinComponent;
            _pinRepository = pinRepository;
        }

        public void RemovePin()
        {
            _pinRoot.SetActive(false);
            _pinComponent.ApplySetPinVisitor(this);
            _pinRepository.Remove(_pin);
            _pinComponent.Dispose();
        }

        public void SetPin(Pin pin) => _pin = pin;
    }
}