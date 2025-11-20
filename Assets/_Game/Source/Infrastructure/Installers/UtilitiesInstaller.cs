using System;
using _Game.Scripts.Core.DI;
using Zenject;
using _Game.Source.Utilities;

namespace _Game.Source.Infrastructure.Installers
{
    public class UtilitiesInstaller: SubInstaller
    {
        public override void InstallBindings(DiContainer Container)
        {
            Container.Bind(typeof(Timer), typeof(IDisposable)).To<Timer>().AsTransient();
        }
    }
}