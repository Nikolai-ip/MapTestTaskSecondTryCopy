using System.Collections.Generic;
using System.Linq;
using _Game.Source.Application.Services.SaveLoadService.SerializeableStructs;
using _Game.Source.Domain;

namespace _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase
{
    public static class PinSerializationConverter
    {
        public static List<SerializablePin> SerializePins(IEnumerable<Pin> pins)
        {
            return pins.Select(ConvertPinToSerializable).ToList();
        }

        public static List<Pin> ConvertPins(IEnumerable<SerializablePin> pins)
        {
            return pins.Select(ConvertSerializableToPin).ToList();
        }
        public static SerializablePin ConvertPinToSerializable(Pin pin)
        {
            return new SerializablePin()
            {
                Id = pin.Id,
                Name = pin.Name,
                Position = new SerializableVector3(pin.Position),
                Description = pin.Description,
                Image = pin.Image
            };
        }

        public static Pin ConvertSerializableToPin(SerializablePin pin)
        {
            return new Pin()
            {
                Id = pin.Id,
                Name = pin.Name,
                Position = pin.Position.UnityVector,
                Description = pin.Description,
                Image = pin.Image
            };
        }
    }
}