using System.IO;
using SFB;

namespace _Game.Source.Application.Services.LoadFile
{
    public class StandaloneFileLoader: IFileLoader
    {
        private readonly string _title;
        private readonly string _startDirectory;
        private readonly ExtensionFilter[] _extensions;
        private readonly string _copyPath;
        public string DirectoryCopyPath => _copyPath;

        public StandaloneFileLoader(string title, string startDirectory, ExtensionFilter[] extensions, string copyPath)
        {
            _title = title;
            _startDirectory = startDirectory;
            _extensions = extensions;
            _copyPath = copyPath;
        }

        public string LoadFile()
        {
            var paths = StandaloneFileBrowser.OpenFilePanel(_title, _startDirectory, _extensions, false);
            string fileName = Path.GetFileName(paths[0]);
            if (!Directory.Exists(_copyPath))
                Directory.CreateDirectory(_copyPath);
            string filePath = Path.Combine(_copyPath, fileName);
            if (!File.Exists(filePath))
                File.Copy(paths[0], filePath);
            return fileName;
        }

        
    }
}