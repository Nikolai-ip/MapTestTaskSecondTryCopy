using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;
using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Source.Application.Services.SaveLoadService.SaveLoadStrategies
{
    public class DataFolderSaver:ISaveLoadStrategy
    {
        private const string SAVE_FOLDER_NAME = "Saves";
        private const string SAVE_FILE_NAME = "GameSaveData.json";
        private static string SaveDataFolder => Path.Combine(UnityEngine.Application.persistentDataPath, SAVE_FOLDER_NAME).Replace("\\", "/");
        private static string SaveFilePath => Path.Combine(SaveDataFolder, SAVE_FILE_NAME).Replace("\\", "/");

        public void CreateSaveFile()
        {
            if (!Directory.Exists(SaveDataFolder))
            {
                Debug.Log("[DataFolderSaver] Creating directory: " + SaveDataFolder);
                Directory.CreateDirectory(SaveDataFolder);
            }
            Debug.Log("[DataFolderSaver] Creating file: " + SaveFilePath);
            File.Create(SaveFilePath).Dispose();
        }

        public bool FileIsNotExist()
        {
            return !File.Exists(SaveFilePath);
        }

        public bool FileIsEmpty()
        {
            return File.ReadAllText(SaveFilePath).Length == 0;
        }

        public void Save(IEnumerable<ISaveLoadObject> objectsToSave)
        {
            try
            {

                var serializedData = objectsToSave.Select(@object => @object.SaveLoadData).ToList();

                if (!Directory.Exists(SaveDataFolder))  
                    Directory.CreateDirectory(SaveDataFolder);
                
                var saveFile = new SaveFile(serializedData);
                var serializedSaveFile = JsonConvert.SerializeObject(saveFile);

                //todo: make async
                File.WriteAllText(SaveFilePath, serializedSaveFile);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public SaveLoadData[] Load()
        {
            if (!File.Exists(SaveFilePath))
            {
                Debug.LogError($"Can't load save file. File {SaveFilePath} is doesn't exist.");
                return null;
            }

            try
            {
                var serializedFile = File.ReadAllText(SaveFilePath);
                if (string.IsNullOrEmpty(serializedFile))
                {
                    Debug.LogError($"Loaded file {SaveFilePath} is empty.");
                    return null;
                }

                Debug.Log($"Load from {SaveFilePath}");
                var loadedData= JsonConvert.DeserializeObject<SaveFile>(serializedFile).Data.ToArray();
                return loadedData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}