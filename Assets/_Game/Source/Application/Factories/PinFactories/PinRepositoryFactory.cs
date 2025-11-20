using System.Collections.Generic;
using System.Linq;
using _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase;
using _Game.Source.Application.Services.SaveLoadService;
using _Game.Source.Domain;
using Zenject;

namespace _Game.Source.Application.Factories.PinFactories
{
    public class PinRepositoryFactory: IFactory<PinRepository>
    {
        private readonly SaveLoadDataStorage<List<SerializablePin>> _savedPins;

        public PinRepositoryFactory(SaveLoadDataStorage<List<SerializablePin>> savedPins)
        {
            _savedPins = savedPins;
        }

        public PinRepository Create()
        {
            var pins = PinSerializationConverter.ConvertPins(_savedPins.Data);
            var pinsMap = pins.ToDictionary(pin => pin.Id, pin => pin);
            return new PinRepository(pinsMap);
        }
    }
}