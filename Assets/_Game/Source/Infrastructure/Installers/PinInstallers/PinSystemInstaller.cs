using _Game.Scripts.Core.DI;
using _Game.Source.Application.Factories.PinFactories;
using _Game.Source.Application.PinUseCases;
using _Game.Source.Application.Validation;
using _Game.Source.Data.StaticData;
using _Game.Source.Domain;
using Plugins.Pool;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.PinInstallers
{
    public class PinSystemInstaller: SubInstaller
    {
        [SerializeField] private float _pinOnValidationDetectRadius;
        [SerializeField] private PoolFactory_SO _pinPoolFactory;
        [SerializeField] private Transform _pinContainer;
        [SerializeField] private DefaultPinData_SO _defaultPinData;
        public override void InstallBindings(DiContainer Container)
        {
            Container.Bind<IInitablePool<PinComponent>>()
                .FromInstance(_pinPoolFactory.GetPoolContainer<PinComponent>(_pinContainer, new ZenjectInstantiator(Container)));
            Container.BindInterfacesAndSelfTo<PinFactory>().AsSingle();
            Container.BindInterfacesTo<RunTimePinCreator>().AsSingle().WithArguments(_defaultPinData);
            Container.BindInterfacesTo<SavedPinCreator>().AsSingle();
            
            Container.BindInterfacesTo<PinRepository>()
                .FromFactory<PinRepository, PinRepositoryFactory>()
                .AsSingle();
            
            Container.Bind<IValidator<PinCanBePlacedContext>>()
                .To<PinCanBePlacedValidator>()
                .AsSingle()
                .WithArguments(_pinOnValidationDetectRadius);
        }
    }
}