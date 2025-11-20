using _Game.Scripts.Core.DI;
using _Game.Source.Application.Factories;
using _Game.Source.Application.SaveLoadUseCases;
using _Game.Source.Application.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.Service
{
    public class SaveLoadServiceInstaller: SubInstaller
    {
        [SerializeField] private float _saveThrottle;
        public override void InstallBindings(DiContainer Container)
        {
            var saveLoadSystem = new SaveLoadSystem();
            Container.Bind<ISaveLoadService>().FromInstance(saveLoadSystem).AsSingle();
            Container.BindInterfacesTo<GameSaver>().AsSingle().WithArguments(_saveThrottle);
            
            BindGameDataStorage(Container, saveLoadSystem);
            saveLoadSystem.LoadGame();
            saveLoadSystem.SaveGame();
        }
        private void BindGameDataStorage(DiContainer container, ISaveLoadService saveLoadService)
        {
            var dataStorages = new GameStorageFactory().GetDataStorages();
            foreach (var dataStorage in dataStorages)
            {
                container.Bind(dataStorage.Key).FromInstance(dataStorage.Value).AsSingle();
            }

            new GameDataStorage(saveLoadService, dataStorages);
        }
    }
}