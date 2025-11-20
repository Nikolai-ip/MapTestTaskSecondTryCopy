using System;
using System.Collections;
using System.Collections.Generic;

namespace _Game.Source.Domain
{
    public class PinRepository: IRepository<Pin>, IRepositoryNotifier<Pin>
    {
        private readonly Dictionary<Guid, Pin> _pinsMap;
        public event Action<Pin> OnElementAppend;
        public event Action<Pin> OnElementRemove;

        public PinRepository(Dictionary<Guid, Pin> pinsMap)
        {
            _pinsMap = pinsMap;
        }

        public void Add(Pin pin)
        {
            _pinsMap.Add(pin.Id, pin);
            OnElementAppend?.Invoke(pin);
        }

        public void Remove(Pin item)
        {
            _pinsMap.Remove(item.Id);
            OnElementRemove?.Invoke(item);
        }

        public IEnumerator<Pin> GetEnumerator() => _pinsMap.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}