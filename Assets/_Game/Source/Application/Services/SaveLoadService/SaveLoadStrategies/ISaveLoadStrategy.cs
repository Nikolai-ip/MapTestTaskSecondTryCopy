using System.Collections.Generic;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;

namespace _Game.Source.Application.Services.SaveLoadService.SaveLoadStrategies
{
    public interface ISaveLoadStrategy
    {
        public void Save(IEnumerable<ISaveLoadObject> objectsToSave);
        public SaveLoadData[] Load();
    }
}