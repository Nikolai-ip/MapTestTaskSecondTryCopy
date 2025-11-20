using UnityEngine;
using Zenject;

namespace _Game.Scripts.Core.DI
{
    public abstract class SubInstaller:MonoBehaviour
    {
        public abstract void InstallBindings(DiContainer Container);
    }
}