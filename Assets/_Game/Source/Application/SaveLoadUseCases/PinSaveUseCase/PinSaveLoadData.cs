using System.Collections.Generic;
using _Game.Source.Application.Services.SaveLoadService.SaveLoadObjects;

namespace _Game.Source.Application.SaveLoadUseCases.PinSaveUseCase
{
    public class PinSaveLoadData: SaveLoadData
    {
        public PinSaveLoadData(List<SerializablePin> pins) : base(new object[]{pins}, nameof(PinSaveLoadData))
        { }
        
    }
}