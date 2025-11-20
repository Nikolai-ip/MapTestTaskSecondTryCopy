using System;
using System.Collections.Generic;
using _Game.Source.Presenter.PinPresentation.Views.Data;
using _Game.Source.Presenter.PinPresentation.Views.PinInfoModeViews;
using _Game.Source.Presenter.UIElements;
using _Game.Source.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presenter.PinPresentation.Views
{
    public class PinInfoWindowView: MonoBehaviour, IViewEnableable<PinInfoViewData>,  IViewInteractable<PinInfoViewCallback>
    {
        [SerializeField] private GameObject _blockPanel;
        [SerializeField] private PopUpWindow _infoWindow;
        [SerializeField] private DictionaryInspector<PinInfoViewMode, PinInfoModeView> _viewsInspector;
        private Dictionary<PinInfoViewMode, PinInfoModeView> _views;
        private PinInfoModeView _currentView;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private ButtonView _editButton;
        [SerializeField] private ButtonView _saveButton;
        [SerializeField] private ButtonView _loadImageButton;
        [SerializeField] private ButtonView _closeButton;
        
        public event Action<PinInfoViewCallback> Callback;

        public void SetData(PinInfoViewData viewData)
        {
            _loadingScreen.SetActive(false);
            _currentView?.Hide();
            _currentView = _views[viewData.ViewMode];
            _currentView.SetData(viewData);
            _currentView.Show();
        }
        private void OnEditButton() => 
            Callback?.Invoke(PinInfoViewCallback.SwithEditMode());

        private void OnLoadImage() =>
            Callback?.Invoke(PinInfoViewCallback.LoadImage(_currentView.Name, _currentView.Description));

        private void OnSaveButton() => 
            Callback?.Invoke(PinInfoViewCallback.OnEditFinished(_views[PinInfoViewMode.Edit].Name, _views[PinInfoViewMode.Edit].Description));

        public void Show()
        {
            _blockPanel.SetActive(true);
            _loadingScreen.SetActive(true);
            _infoWindow.Show();
        }

        public void Hide()
        {
            _blockPanel.SetActive(false);
            _infoWindow.Hide();
            foreach (var view in _views.Values)
            {
                view.Dispose();
            }
        }

        private void OnCloseButtonClicked() => Callback?.Invoke(PinInfoViewCallback.OnClose());

        private void OnEnable()
        {
            _views = _viewsInspector.GetDictionary();
            foreach (var view in _views.Values)
                view.Hide();
            
            _editButton.Action += OnEditButton;
            _loadImageButton.Action += OnLoadImage;
            _saveButton.Action += OnSaveButton;
            _closeButton.Action += OnCloseButtonClicked;
        }


        private void OnDisable()
        {
            _editButton.Action -= OnEditButton;
            _loadImageButton.Action -= OnLoadImage;
            _saveButton.Action -= OnSaveButton;
            _closeButton.Action -= OnCloseButtonClicked;
        }
    }
}