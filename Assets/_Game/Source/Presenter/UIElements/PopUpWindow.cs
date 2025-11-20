using Plugins.DOTweenFramework;
using UnityEngine;

namespace _Game.Source.Presenter.UIElements
{
    public class PopUpWindow: MonoBehaviour, IViewEnableable
    {
        [SerializeField] private TweenComponent _showAnimation;
        [SerializeField] private TweenComponent _hideAnimation;
        [SerializeField] private bool _useHideAnimation;
        [SerializeField] private GameObject _window;
        
        public void Show()
        {
            _window.SetActive(true);
            _showAnimation.Play();
        }

        public void Hide()
        {
            if (_useHideAnimation)
                _hideAnimation.Play();
            else
                DisableWindow();
        }
        private void DisableWindow()
        {
            _window.SetActive(false);
        }
        private void OnEnable()
        {
            if (_useHideAnimation)
                _hideAnimation.OnFinished += DisableWindow;
        }

        private void OnDisable()
        {
            if (_useHideAnimation)
                _hideAnimation.OnFinished -= DisableWindow;
        }


    }
}