using _Game.Source.Application.PinUseCases;
using _Game.Source.Presenter.PinPresentation;
using _Game.Source.Presenter.PinPresentation.Views;
using _Game.Source.Presenter.UIElements;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.PinInstallers
{
    public class PinInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _pinRoot;
        [SerializeField] private PinView _view;
        [SerializeField] private ButtonView _moreInfoButton;
        [SerializeField] private ButtonView _removePinButton;
        [SerializeField] private PinComponent _pinComponent;
        public override void InstallBindings()
        {
            Container.Bind<PinComponent>().FromInstance(_pinComponent).AsSingle();
            Container.BindInterfacesTo<PinView>().FromInstance(_view).AsSingle();
            Container.BindInterfacesTo<PinPresenter>().AsSingle().WithArguments(_moreInfoButton, _removePinButton);
            Container.Bind<IPinRemover>().To<PinDisabler>().AsSingle().WithArguments(_pinRoot);
        }
    }
}