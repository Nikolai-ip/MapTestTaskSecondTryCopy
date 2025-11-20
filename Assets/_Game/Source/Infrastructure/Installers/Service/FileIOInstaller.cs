using _Game.Scripts.Core.DI;
using _Game.Source.Application.Services.LoadFile;
using _Game.Source.Data.StaticData;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.Service
{
    public class FileIOInstaller: SubInstaller
    {
        [SerializeField] private StandaloneFileLoaderFactory_SO _loaderFactory;
        public override void InstallBindings(DiContainer Container)
        {
            Container.Bind<IFileLoader>().FromInstance(_loaderFactory.Create()).AsSingle();
            Container.Bind<TexturesDataBase>().AsSingle();
        }
    }
}