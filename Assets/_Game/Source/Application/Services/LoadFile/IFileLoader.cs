namespace _Game.Source.Application.Services.LoadFile
{
    public interface IFileLoader
    {
        string LoadFile();
        string DirectoryCopyPath { get; }
    }
}