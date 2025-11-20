using System.Linq;
using Zenject;

namespace _Game.Scripts.Core.DI
{
    public class AutoInstallerBundle: MonoInstaller
    {
        public override void InstallBindings()
        {
            var installers = GetComponentsInChildren<SubInstaller>().ToList();
            installers.ForEach(installer=>installer.InstallBindings(Container));
        }
    }
}