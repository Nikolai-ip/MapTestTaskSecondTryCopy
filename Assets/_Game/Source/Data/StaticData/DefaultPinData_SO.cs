using UnityEngine;

namespace _Game.Source.Data.StaticData
{
    [CreateAssetMenu(fileName = "DefaultPinData", menuName = "StaticData/Pin/DefaultPinData")]
    public class DefaultPinData_SO: ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
    }
}