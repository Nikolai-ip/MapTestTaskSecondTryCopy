using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;

namespace _Game.Source.Application.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void RegisterSaveLoadObject(ISaveLoadObject saveObject);
        void UnregisterSaveLoadObject(ISaveLoadObject saveObject);
        void SaveGame();
        void LoadGame();
    }

    public interface IRemotingSaveLoadSystem
    {
        void RegisterSaveLoadHandler<TSaveLoadObj>(TSaveLoadObj saveObject) where TSaveLoadObj : ISaveLoadObject;
        bool Save<TSaveLoadObj>(TSaveLoadObj saveObject) where TSaveLoadObj : ISaveLoadObject;
    }
}