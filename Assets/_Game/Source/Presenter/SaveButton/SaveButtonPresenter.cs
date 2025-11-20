using System;
using _Game.Source.Infrastructure.Signals;
using _Game.Source.Presenter.UIElements;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace _Game.Source.Presenter.SaveButton
{
    public class SaveButtonPresenter: MonoBehaviour
    {
        [Inject] private IPublisher<SaveGameSignal> _saveGamePublisher;
        [SerializeField] private ButtonView _saveButton;

        private void OnEnable()
        {
            _saveButton.Action += SendSaveGameSignal;
        }

        private void OnDisable()
        {
            _saveButton.Action -= SendSaveGameSignal;
        }

        private void SendSaveGameSignal()
        {
            _saveGamePublisher.Publish(new SaveGameSignal());
        }
    }
}