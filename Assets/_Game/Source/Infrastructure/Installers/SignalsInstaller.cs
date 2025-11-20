using _Game.Scripts.Core.DI;
using _Game.Source.Infrastructure.Signals;
using MessagePipe;
using Zenject;

namespace _Game.Source.Infrastructure.Installers
{
    public class SignalsInstaller: SubInstaller
    {
        public override void InstallBindings(DiContainer Container)
        {
            var option = Container.BindMessagePipe();
            Container.BindMessageBroker<ShowMoreInfoSignal>(option);
            Container.BindMessageBroker<SaveGameSignal>(option);
            Container.BindMessageBroker<PinDataChanged>(option);
        }
    }
}