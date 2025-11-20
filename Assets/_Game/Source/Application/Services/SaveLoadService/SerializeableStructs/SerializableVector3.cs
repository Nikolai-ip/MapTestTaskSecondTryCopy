using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Source.Application.Services.SaveLoadService.SerializeableStructs
{
    [System.Serializable]
    public class SerializableVector3
    {
        public float X;
        public float Y;
        public float Z;

        [JsonIgnore]
        public Vector3 UnityVector => new(X, Y, Z);
        
        public SerializableVector3(Vector3 v){
            X = v.x;
            Y = v.y;
            Z = v.z;
        }
    }
}