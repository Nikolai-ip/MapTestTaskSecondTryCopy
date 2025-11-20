using System;
using System.Collections.Generic;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadStrategies;
using UnityEngine;

namespace _Game.Source.Application.Services.SaveLoadService
{
    public class SaveLoadSystem:ISaveLoadService
    {
        private readonly Dictionary<Type, object> DataSavers;
        private readonly DataFolderSaver _dataFolderSaver = new();
        private readonly Dictionary<string, ISaveLoadObject> _componentsIdToSaveObject = new();

        public event Action OnLoadObject;
        public int MaxLoadObjects => _componentsIdToSaveObject.Values.Count;

        public void SaveGame()
        {
            SaveAll(_dataFolderSaver);
        }

        private void SaveAll(ISaveLoadStrategy saver)
        {
            Save(saver, _componentsIdToSaveObject.Values);
        }

        private void Save(ISaveLoadStrategy saver, IEnumerable<ISaveLoadObject> data)
        {
            saver.Save(data);
        }

        public void LoadGame()
        {
            try
            {
                if (IsFirstLogging)
                {
                    InitSaveDirectory();
                }
                Load(_dataFolderSaver);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

        }

        private bool IsFirstLogging => _dataFolderSaver.FileIsNotExist() || _dataFolderSaver.FileIsEmpty();


        private void InitSaveDirectory()
        {
            _dataFolderSaver.CreateSaveFile();
            SaveGame();
        }

        private void Load(ISaveLoadStrategy loader)
        {
            var loadedData = loader.Load();

            for (var i = 0; i < loadedData.Length; i++)
            {
                var data = loadedData[i];
                var objectId = data.Id;
                if (!_componentsIdToSaveObject.ContainsKey(objectId))
                {
                    Debug.LogError($"Can't restore data for object with id {objectId}");
                    continue;
                }
                
                _componentsIdToSaveObject[objectId].RestoreValues(data);
                OnLoadObject?.Invoke();
            }
        }

        public void RegisterSaveLoadObject(ISaveLoadObject saveObject)
        {
            _componentsIdToSaveObject[saveObject.ComponentID] = saveObject;
        }

        public void UnregisterSaveLoadObject(ISaveLoadObject saveObject)
        {
            _componentsIdToSaveObject.Remove(saveObject.ComponentID);
        }
    }
}