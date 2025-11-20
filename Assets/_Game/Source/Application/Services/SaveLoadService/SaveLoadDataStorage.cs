using System;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;
using Newtonsoft.Json.Linq;

namespace _Game.Source.Application.Services.SaveLoadService
{
    public class SaveLoadDataStorage<TData>: ISaveLoadObject, ISaveLoadDB<TData>
    {
        public string ComponentID => SaveLoadData.GetType().Name;
        public SaveLoadData SaveLoadData { get; }
        public TData Data => GetSavedData<TData>();

        public SaveLoadDataStorage(SaveLoadData saveLoadData)
        {
            SaveLoadData = saveLoadData;
        }
        private TSaved GetSavedData<TSaved>()
        {
            if (SaveLoadData.Data[0] is TSaved savedData)
                return savedData;
            
            throw new ArgumentException($"Invalid saved data type: {typeof(TSaved)}. The real type is: {SaveLoadData.GetType()}");
        }
        
        public void RestoreValues(SaveLoadData saveLoadData)
        {
            RestoreValueByType<TData>(saveLoadData);
        }

        private TSaved RestoreValueByType<TSaved>(SaveLoadData saveLoadData)
        {
            JToken token = (JToken)saveLoadData.Data[0];
            var restoredData = token.ToObject<TSaved>();
            SaveLoadData.Update(restoredData);
            return restoredData;
        }
        
        public virtual ISaveLoadObject Clone()
        {
            return MemberwiseClone() as SaveLoadDataStorage<TData>;
        }
    }
}