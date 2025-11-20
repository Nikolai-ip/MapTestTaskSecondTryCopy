using _Game.Scripts.Core.DI;
using _Game.Source.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.Service
{
    public class InputInstaller: SubInstaller
    {
        public override void InstallBindings(DiContainer Container)
        {
            Container.BindInterfacesTo<PCInputService>().AsSingle().WithArguments(Camera.main);
        }
    }
}