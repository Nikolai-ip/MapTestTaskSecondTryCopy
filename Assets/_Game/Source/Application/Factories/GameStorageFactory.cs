using System;
using System.Collections.Generic;
using _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase;
using _Game.Source.Application.Services.SaveLoadService;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;

namespace _Game.Source.Application.Factories
{
    public class GameStorageFactory
    {
        public Dictionary<Type, ISaveLoadObject> GetDataStorages()
        {
            return new Dictionary<Type, ISaveLoadObject>()
            {
                {typeof(SaveLoadDataStorage<List<SerializablePin>>),
                    new SaveLoadDataStorage<List<SerializablePin>>(new PinSaveLoadData(new List<SerializablePin>()))}
            };
        }
    }
}