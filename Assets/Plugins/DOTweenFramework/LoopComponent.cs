using DG.Tweening;
using UnityEngine;

namespace Plugins.DOTweenFramework
{
    public class LoopComponent: TweenComponent
    {
        [SerializeField] private TweenComponent component;
        [SerializeField] private LoopType _loopType;

        public override Tween Play()
        {
            Tween = component.Play().SetLoops(-1, _loopType);
            return Tween;
        }
    }
}