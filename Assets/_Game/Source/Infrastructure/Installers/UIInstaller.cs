using _Game.Scripts.Core.DI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Game.Source.Infrastructure.Installers
{
    public class UIInstaller: SubInstaller
    {
        [SerializeField] private EventSystem _eventSystem;
        public override void InstallBindings(DiContainer Container)
        {
            Container.Bind<EventSystem>().FromInstance(_eventSystem).AsSingle();
        }
    }
}