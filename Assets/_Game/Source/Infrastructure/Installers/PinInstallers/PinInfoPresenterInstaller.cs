using _Game.Scripts.Core.DI;
using _Game.Source.Presenter.PinPresentation;
using _Game.Source.Presenter.PinPresentation.Views;
using UnityEngine;
using Zenject;

namespace _Game.Source.Infrastructure.Installers.PinInstallers
{
    public class PinInfoPresenterInstaller: SubInstaller
    {
        [SerializeField] private PinInfoWindowView _view;
        public override void InstallBindings(DiContainer Container)
        {
            Container.BindInterfacesTo<PinInfoPresenter>().AsSingle().WithArguments(_view, _view);
        }
    }
}