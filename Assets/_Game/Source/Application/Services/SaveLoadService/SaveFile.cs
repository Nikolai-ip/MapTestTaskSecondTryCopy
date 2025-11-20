using System;
using System.Collections.Generic;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;

namespace _Game.Source.Application.Services.SaveLoadService
{
    [Serializable]
    public struct SaveFile
    { 
        public DateTime SaveTime { get; }
        public List<SaveLoadData> Data { get; }

        public SaveFile(List<SaveLoadData> data) : this()
        {
            Data = data;
            SaveTime = DateTime.Now;
        }
    }
}