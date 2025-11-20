using System;
using _Game.Source.Application.PinUseCases;
using _Game.Source.Application.Services.LoadFile;
using _Game.Source.Infrastructure.Signals;
using _Game.Source.Presenter.PinPresentation.Views.Data;
using MessagePipe;
using Zenject;

namespace _Game.Source.Presenter.PinPresentation
{
    public class PinInfoPresenter: IInitializable, IDisposable
    {
        private readonly ISubscriber<ShowMoreInfoSignal> _showMoreInfoSignal;
        private IDisposable _subscription;
        private readonly IViewEnableable<PinInfoViewData> _pinInfoView;
        private readonly IViewInteractable<PinInfoViewCallback> _pinInfoViewInteractable;
        private readonly TexturesDataBase _textureDataBase;
        private PinInfoViewMode _viewMode;
        private PinComponent _currentPin;

        public PinInfoPresenter(ISubscriber<ShowMoreInfoSignal> showMoreInfoSignal, IViewEnableable<PinInfoViewData> pinInfoView, IViewInteractable<PinInfoViewCallback> pinInfoViewInteractable, TexturesDataBase textureDataBase)
        {
            _showMoreInfoSignal = showMoreInfoSignal;
            _pinInfoView = pinInfoView;
            _pinInfoViewInteractable = pinInfoViewInteractable;
            _textureDataBase = textureDataBase;
        }

        public void Initialize()
        {
            _subscription = _showMoreInfoSignal.Subscribe(ShowInfoView);
            _pinInfoViewInteractable.Callback += HandleViewCallback;
        }

        private void ShowInfoView(ShowMoreInfoSignal signal)
        {
            _currentPin = signal.Pin;
            _pinInfoView.Show();
            UpdateView();
        }
        private async void UpdateView()
        {
            var loadTextureResult = await _textureDataBase.TryGetTexture(_currentPin.Pin.Image);
            
            _pinInfoView.SetData(new PinInfoViewData(_currentPin.Pin.Name, _currentPin.Pin.Description,
                loadTextureResult.Texture, loadTextureResult.IsSuccess, _viewMode));
        }

        private void HandleViewCallback(PinInfoViewCallback callback)
        {
            switch (callback.Action)
            {
                case PinInfoViewCallback.ActionType.Edit: 
                    OnSwitchMode();
                    break;
                case PinInfoViewCallback.ActionType.EditFinished:
                    SetNewData(callback);
                    OnSwitchMode();
                    break;
                case PinInfoViewCallback.ActionType.Close: 
                    OnCloseInfoView();
                    break;
                case PinInfoViewCallback.ActionType.LoadImage:
                    SetNewData(callback);
                    OnLoadImage();
                    break;
            }
        }

        private async void OnLoadImage()
        {
            string imageName = await _textureDataBase.LoadImageInToProject();
            _currentPin.SetImage(imageName);
            UpdateView();
        }

        private void SetNewData(PinInfoViewCallback callback)
        {
            _currentPin.SetNewTextData(callback.NewName, callback.NewDescription);
        }

        private void OnSwitchMode()
        {
            if (_viewMode == PinInfoViewMode.Read)
                _viewMode = PinInfoViewMode.Edit;
            else
                _viewMode = PinInfoViewMode.Read;
            UpdateView();
        }

        private void OnCloseInfoView()
        {
            _viewMode = PinInfoViewMode.Read;
            _currentPin = null;
            _pinInfoView.Hide();
        }

        public void Dispose()
        {
            _subscription.Dispose();
            _pinInfoViewInteractable.Callback -= HandleViewCallback;
        }
    }
}