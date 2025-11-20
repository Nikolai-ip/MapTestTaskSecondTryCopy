using System;
using _Game.Source.Application.Services.SaveLoadService.SerializeableStructs;

namespace _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase
{
    [Serializable]
    public class SerializablePin
    {
        public Guid Id { get; set; }
        public SerializableVector3 Position { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}