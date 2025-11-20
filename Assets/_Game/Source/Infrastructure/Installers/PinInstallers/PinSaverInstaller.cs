using _Game.Scripts.Core.DI;
using _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.PinInstallers
{
    public class PinSaverInstaller: SubInstaller
    {
        public override void InstallBindings(DiContainer Container)
        {
            Container.BindInterfacesTo<PinsSaver>().AsSingle();
        }
    }
}