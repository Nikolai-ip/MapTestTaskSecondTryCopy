using System;
using _Game.Source.Application.PinUseCases;
using _Game.Source.Infrastructure.Signals;
using _Game.Source.Presenter.PinPresentation.Views.Data;
using MessagePipe;
using Zenject;

namespace _Game.Source.Presenter.PinPresentation
{
    public class PinPresenter : IInitializable, IDisposable
    {
        private readonly PinComponent _pinComponent;
        private readonly IView<PinViewData> _view;
        private readonly IViewInteractable<PinViewCallBack> _viewCallBack;
        private readonly IViewAction _moreInfoButton;
        private readonly IViewAction _removePinButton;
        private readonly IPinRemover _pinRemover;
        private readonly IPublisher<ShowMoreInfoSignal> _showMoreInfoSignalPublisher;
        private bool _isLabelEnabled;

        public PinPresenter(PinComponent pinComponent, IView<PinViewData> view,
            IViewInteractable<PinViewCallBack> viewCallBack, IViewAction moreInfoButton, IViewAction removePinButton,
            IPublisher<ShowMoreInfoSignal> showMoreInfoSignalPublisher, IPinRemover pinRemover)
        {
            _pinComponent = pinComponent;
            _view = view;
            _viewCallBack = viewCallBack;
            _moreInfoButton = moreInfoButton;
            _showMoreInfoSignalPublisher = showMoreInfoSignalPublisher;
            _pinRemover = pinRemover;
            _removePinButton = removePinButton;
        }

        public void Initialize()
        {
            _pinComponent.OnInit += UpdatePosition;
            _pinComponent.OnPinTextDataChanged += UpdatePinTextDataView;
            _viewCallBack.Callback += HandleViewCallback;
            _moreInfoButton.Action += PublishShowMoreInfoSignal;
            _removePinButton.Action += OnRemovePin;
        }

        private void UpdatePinTextDataView(ReadOnlyPin pin)
        {
            _view.SetData(PinViewData.SetLabelData(_isLabelEnabled, _pinComponent.Pin.Name));
        }

        private void OnRemovePin()
        {
            _pinRemover.RemovePin();
        }

        private void UpdatePosition()
        {
            _view.SetData(PinViewData.OnSetPosition(_pinComponent.Pin.Position));
        }

        private void HandleViewCallback(PinViewCallBack callBack)
        {
            switch (callBack.Action)
            {
                case PinViewCallBack.ActionType.ChangePosition:
                    OnPinPositionChanged(callBack);
                    break;
                case PinViewCallBack.ActionType.LabelToggle:
                    OnLabelToggled();
                    break;
            }
        }

        private void OnPinPositionChanged(PinViewCallBack callBack)
        {
            _pinComponent.SetPosition(callBack.Position);
        }

        private void OnLabelToggled()
        {
            _isLabelEnabled = !_isLabelEnabled;
            _view.SetData(PinViewData.SetLabelData(_isLabelEnabled, _pinComponent.Pin.Name));
        }
        private void PublishShowMoreInfoSignal()
        {
            _showMoreInfoSignalPublisher.Publish(new ShowMoreInfoSignal(_pinComponent));
        }
        public void Dispose()
        {
            _pinComponent.OnInit -= UpdatePosition;
            _pinComponent.OnPinTextDataChanged -= UpdatePinTextDataView;
            _viewCallBack.Callback -= HandleViewCallback;
            _moreInfoButton.Action -= PublishShowMoreInfoSignal;
            _removePinButton.Action -= OnRemovePin;
        }
    }
}