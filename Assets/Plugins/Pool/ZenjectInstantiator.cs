using UnityEngine;
using Zenject;

namespace Plugins.Pool
{
    public class ZenjectInstantiator : IObjectInstantiator
    {
        readonly DiContainer _diContainer;

        public ZenjectInstantiator(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Instantiate<T>(T prefab, Transform container) where T : MonoBehaviour
        {
            return _diContainer.InstantiatePrefab(prefab, container).GetComponent<T>();
        }
    }
}