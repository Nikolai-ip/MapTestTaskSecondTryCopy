using System;
using _Game.Source.Application.PinUseCases;
using _Game.Source.Domain;
using Plugins.Pool;
using Zenject;

namespace _Game.Source.Application.Factories.PinFactories
{
    public class PinFactory: IInitializable
    {
        private readonly IInitablePool<PinComponent> _pinPool;

        public PinFactory(IInitablePool<PinComponent> pinPool)
        {
            _pinPool = pinPool;
        }
        public void Initialize()
        {
            _pinPool.Init();
        }

        public void Create(Func<Pin> pinFactory)
        {
            var pin = _pinPool.GetElement();
            pin.Initialize(pinFactory.Invoke());
        }
    }
}