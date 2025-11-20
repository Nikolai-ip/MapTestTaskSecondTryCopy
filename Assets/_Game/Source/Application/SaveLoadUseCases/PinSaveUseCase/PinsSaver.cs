using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Source.Application.Services.SaveLoadService;
using _Game.Source.Application.Services.SaveLoadService.SerializeableStructs;
using _Game.Source.Domain;
using _Game.Source.Infrastructure.Signals;
using MessagePipe;
using Zenject;

namespace _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase
{
    public class PinsSaver: IInitializable, IDisposable
    {
        private readonly IRepository<Pin> _pinRepository;
        private readonly IRepositoryNotifier<Pin>  _pinRepositoryNotifier;
        private readonly SaveLoadDataStorage<List<SerializablePin>> _saveLoadDataStorage;
        private readonly ISubscriber<PinDataChanged> _pinDataChangedSubscriber;
        private IDisposable _sub;
        
        public PinsSaver(IRepository<Pin> pinRepository, IRepositoryNotifier<Pin> pinRepositoryNotifier, SaveLoadDataStorage<List<SerializablePin>> saveLoadDataStorage, ISubscriber<PinDataChanged> pinDataChangedSubscriber)
        {
            _pinRepository = pinRepository;
            _pinRepositoryNotifier = pinRepositoryNotifier;
            _saveLoadDataStorage = saveLoadDataStorage;
            _pinDataChangedSubscriber = pinDataChangedSubscriber;
        }

        public void Initialize()
        {
            _pinRepositoryNotifier.OnElementAppend += AddPinToDb;
            _pinRepositoryNotifier.OnElementRemove += RemovePinFromDb;
            _sub = _pinDataChangedSubscriber.Subscribe(UpdatePinData);
        }

        private void UpdatePinData(PinDataChanged signal)
        {
            Pin pin = signal.Pin;
            SerializablePin serializablePin = _saveLoadDataStorage.Data.Find(srPin => srPin.Id == pin.Id);
            if (serializablePin != null)
            {
                serializablePin.Image = pin.Image;
                serializablePin.Name = pin.Name;
                serializablePin.Description = pin.Description;
                serializablePin.Position = new SerializableVector3(pin.Position);
            }

        }
        private void AddPinToDb(Pin pin)
        {
            if (!_saveLoadDataStorage.Data.Any(sp => sp.Id.Equals(pin.Id)))
            {
                _saveLoadDataStorage.Data.Add(PinSerializationConverter.ConvertPinToSerializable(pin));
            }
        }

        private void RemovePinFromDb(Pin pin)
        {
            int itemIndex = _saveLoadDataStorage.Data.FindIndex(sp => sp.Id.Equals(pin.Id));
            if (itemIndex != -1) _saveLoadDataStorage.Data.RemoveAt(itemIndex);
        }


        public void Dispose()
        {
            _pinRepositoryNotifier.OnElementAppend -= AddPinToDb;
            _pinRepositoryNotifier.OnElementRemove -= RemovePinFromDb;
            _sub.Dispose();
        }
    }
}