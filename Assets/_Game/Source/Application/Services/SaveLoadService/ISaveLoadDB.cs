namespace _Game.Source.Application.Services.SaveLoadService
{
    public interface ISaveLoadDB<TData>
    {
        TData Data { get; }
    }
}