using System;

namespace _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects
{
    [Serializable]
    public class SaveLoadData
    {
        public SaveLoadData(object[] data, string id)
        {
            Data = data;
            Id = id;
        }

        public virtual void Update<TSaved>(TSaved data)
        {
            Data = new object[]{data};
        }
        public object[] Data { get; protected set; }
        public string Id { get; protected set; }
    }
}