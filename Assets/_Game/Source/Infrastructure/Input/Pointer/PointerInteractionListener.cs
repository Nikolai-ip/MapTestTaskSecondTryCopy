using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Game.Source.Infrastructure.Input.Pointer
{
    public class PointerInteractionListener: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler 
    {
        [SerializeField] private float pointerDownInterval = 0.2f;
        [SerializeField] private float pointerUpInterval = 0.2f;
        [SerializeField] private bool _debug;
        
        [Header("Events with PointerEventData argument")]
        public UnityEvent<PointerEventData> onPointerDownEventData;
        public UnityEvent<PointerEventData> onPointerUpEventData;
        public UnityEvent<PointerEventData> onBeginDragEventData;
        public UnityEvent<PointerEventData> onDragEventData;
        public UnityEvent<PointerEventData> onEndDragEventData;

        [Header("Events with empty arguments")]
        public UnityEvent onPointerDown;
        public UnityEvent onPointerUp;
        public UnityEvent onBeginDrag;
        public UnityEvent onDrag;
        public UnityEvent onEndDrag;
        
        private readonly Subject<PointerEventData> _pointerDownSubject = new();
        private readonly Subject<PointerEventData> _pointerUpSubject = new();
        private CompositeDisposable _compositeDisposable;

        private void OnEnable()
        {
            _compositeDisposable = new();
            _pointerDownSubject.ThrottleFirst(TimeSpan.FromSeconds(pointerDownInterval)).Subscribe((e) =>
            {
                onPointerDownEventData.Invoke(e);
                onPointerDown.Invoke();
                if (_debug) Debug.Log("[PointerInteractionListener] OnPointerDown");
            }).AddTo(_compositeDisposable);
            
            _pointerUpSubject.ThrottleFirst(TimeSpan.FromSeconds(pointerUpInterval)).Subscribe((e) =>
            {
                onPointerUpEventData.Invoke(e);
                onPointerUp.Invoke();
                if (_debug) Debug.Log("[PointerInteractionListener] OnPointerUp");
            }).AddTo(_compositeDisposable);
        }

        public void OnPointerDown(PointerEventData e) => _pointerDownSubject.OnNext(e);
        public void OnPointerUp(PointerEventData e) => _pointerUpSubject.OnNext(e);
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDragEventData.Invoke(eventData);
            onBeginDrag.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDragEventData.Invoke(eventData);
            onDrag.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDragEventData.Invoke(eventData);
            onEndDrag.Invoke();
        }

        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
        }
    }
}