using System;
using _Game.Source.Application.Services.SaveLoadService;
using _Game.Source.Infrastructure.Signals;
using MessagePipe;
using UniRx;
using Zenject;

namespace _Game.Source.Application.SaveLoadUseCases
{
    public class GameSaver: IInitializable, IDisposable
    {
        private readonly ISubscriber<SaveGameSignal> _saveGameSignal;
        private readonly ISaveLoadService _saveLoadService;
        private readonly float _saveGameThrottle;

        private IDisposable _signalSub;
        private IDisposable _streamDisposable;
        private readonly Subject<Unit> _onGameSignalSubject = new();
        private IObservable<Unit> _saveStream;

        public GameSaver(ISubscriber<SaveGameSignal> saveGameSignal, ISaveLoadService saveLoadService, float saveGameThrottle)
        {
            _saveGameSignal = saveGameSignal;
            _saveLoadService = saveLoadService;
            _saveGameThrottle = saveGameThrottle;
        }

        public void Initialize()
        {
            _signalSub = _saveGameSignal.Subscribe(OnSaveGameSignal);
            _streamDisposable = _onGameSignalSubject
                .AsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(_saveGameThrottle))
                .Subscribe((u)=>_saveLoadService.SaveGame());
        }

        private void OnSaveGameSignal(SaveGameSignal signal)
        {
            _onGameSignalSubject.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _streamDisposable?.Dispose();
            _signalSub?.Dispose();
            _onGameSignalSubject.Dispose();
        }
    }
}