using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Source.Utilities
{
    public class Timer: IDisposable
    {
        private CancellationTokenSource _timerCts;
        public async void Start(Action<float> onTick)
        {
            try
            {
                _timerCts = new();
                while (!_timerCts.IsCancellationRequested)
                {
                    onTick.Invoke(Time.deltaTime);
                    await UniTask.Yield(PlayerLoopTiming.Update, _timerCts.Token);
                }
            }
            catch (OperationCanceledException){}
            catch (Exception e) { Debug.LogException(e); }
        }

        public async void Stop()
        {
            if (_timerCts == null) return;
            _timerCts.Cancel();
            await UniTask.Yield();
            Dispose();
        }

        public void Dispose()
        {
            _timerCts?.Dispose();
        }
    }
}