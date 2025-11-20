using System;
using _Game.Source.Infrastructure.Input;
using _Game.Source.Presenter.PinPresentation.Views.Data;
using Plugins.DOTweenFramework;
using UnityEngine;
using Zenject;
using Timer = _Game.Source.Utilities.Timer;

namespace _Game.Source.Presenter.PinPresentation.Views
{
    public class PinView: MonoBehaviour, IView<PinViewData>, IViewInteractable<PinViewCallBack>
    {
        public event Action<PinViewCallBack> Callback;
        [SerializeField] private Transform _pinTr;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private float _selectDelayToMove;
        [SerializeField] private PinLabelPopUp _labelWindow;
        [SerializeField] private TweenComponent _highlightAnimation;
        
        private float _selectElapsedTime;
        private IInputService _inputService;
        private Timer _selectTimer;
        private bool _isLabelEnabledCached;
        
        [Inject]
        public void Construct(IInputService inputService, Timer timer)
        {
            _inputService = inputService;
            _selectTimer = timer;
        }

        private void OnEnable() => _labelWindow.Hide();

        public void SetData(PinViewData data)
        {
            if (data.SetPosition)
                _pinTr.position = (Vector2)data.Position + _offset;
            
            if (data.IsLabelEnabled)
                _labelWindow.SetName(data.Name);

            if (_isLabelEnabledCached != data.IsLabelEnabled)
            {
                _isLabelEnabledCached  = data.IsLabelEnabled;
                if (_isLabelEnabledCached)
                    _labelWindow.Show();
                else
                    _labelWindow.Hide();
            }
        }

        public void OnPointerDown()
        {
            _highlightAnimation.Play();
            _selectTimer.Start((dt) =>
            {
                _selectElapsedTime += dt;
                if (_selectElapsedTime >= _selectDelayToMove)
                {
                    MovePin();
                }
            });
        }
        
        private void MovePin()
        {
            _pinTr.position = _inputService.PointerPosition + _offset;
        }

        public void OnPointerUp()
        {
            _highlightAnimation.PlayBackwards();
            _selectTimer.Stop();
            _selectElapsedTime = 0;
            var callBackType = _selectElapsedTime >= _selectDelayToMove? 
                PinViewCallBack.ActionType.ChangePosition: 
                PinViewCallBack.ActionType.LabelToggle;
            var pinViewCallback = new PinViewCallBack(callBackType)
            {
                Position = _pinTr.position
            };
            Callback?.Invoke(pinViewCallback);
        }
        
    }
}