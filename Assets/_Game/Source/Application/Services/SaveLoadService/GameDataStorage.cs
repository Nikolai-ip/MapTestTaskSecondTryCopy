using System;
using System.Collections.Generic;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;

namespace _Game.Source.Application.Services.SaveLoadService
{
    public class GameDataStorage
    {
        private readonly Dictionary<Type, ISaveLoadObject> _storage;

        private readonly ISaveLoadService _saveLoadService;

        public T GetDataStorage<T>() where T:ISaveLoadObject
        {
            if (_storage.TryGetValue(typeof(T), out var saveLoadObject))
            {
                return (T)saveLoadObject;
            }
            throw new KeyNotFoundException($"There is no save load object {typeof(T).Name}");
        }
        
        public GameDataStorage(ISaveLoadService saveLoadService, Dictionary<Type, ISaveLoadObject> storage)
        {
            _storage = storage;
            _saveLoadService = saveLoadService;
            RegistrationSaveLoadObjects();
        }
        
        private void RegistrationSaveLoadObjects()
        {
            foreach (var saveLoadObject in _storage)
                _saveLoadService.RegisterSaveLoadObject(saveLoadObject.Value);
            
        }
        private void UnregistrationSaveLoadObjects()
        {
            foreach (var saveLoadObject in _storage)
                _saveLoadService.UnregisterSaveLoadObject(saveLoadObject.Value);
            
        }
        
        private Dictionary<Type, ISaveLoadObject> CopyStorageDictionary()
        {
            Dictionary<Type, ISaveLoadObject> copyStorageDictionary = new();
            foreach (var kvp in _storage)
            {
                copyStorageDictionary.Add(kvp.Key, kvp.Value.Clone());
            }

            return copyStorageDictionary;
        }
    }
    
}