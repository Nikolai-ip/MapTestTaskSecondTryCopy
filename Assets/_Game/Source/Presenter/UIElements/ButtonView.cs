using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Presenter.UIElements
{
    public class ButtonView: MonoBehaviour, IViewAction
    {
        public event Action Action;
        private Button _button;
        [SerializeField] private float _throttle = 0.2f;
        private IDisposable _sub;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _sub = _button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(_throttle))
                .Subscribe(_ => { Action?.Invoke(); });
        }

        private void OnDisable()
        {
            _sub?.Dispose();
        }
    }
}