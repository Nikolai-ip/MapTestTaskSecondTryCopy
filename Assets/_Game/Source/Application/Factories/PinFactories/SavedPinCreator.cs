using _Game.Source.Domain;
using Zenject;

namespace _Game.Source.Application.Factories.PinFactories
{
    public class SavedPinCreator: IInitializable
    {
        private readonly PinFactory _pinFactory;
        private readonly IRepository<Pin> _pinRepository;

        public SavedPinCreator(PinFactory pinFactory, IRepository<Pin> pinRepository)
        {
            _pinFactory = pinFactory;
            _pinRepository = pinRepository;
        }

        public void Initialize()
        {
            foreach (var pin in _pinRepository)
            {
                _pinFactory.Create(()=> pin);
            }
        }
    }
}