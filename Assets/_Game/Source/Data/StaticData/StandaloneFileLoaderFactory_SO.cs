using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Game.Source.Application.Services.LoadFile;
using SFB;
using UnityEngine;
using Zenject;

namespace _Game.Source.Data.StaticData
{
    [CreateAssetMenu(menuName = "StaticFactories/StandaloneFileLoaderFactory")]
    public class StandaloneFileLoaderFactory_SO: ScriptableObject, IFactory<IFileLoader>
    {
        [SerializeField] private string _title;
        [SerializeField] private string _startDirectory;
        [SerializeField] private string _copyFolderName;
        [SerializeField] private List<ExtensionFilterSerializable> _extensions;

        public IFileLoader Create()
        {
            var extensionFilters = _extensions.Select(efs => new ExtensionFilter(efs.Name, efs.Extensions)).ToArray();
            return new StandaloneFileLoader(_title, _startDirectory, extensionFilters, 
                Path.Combine(UnityEngine.Application.persistentDataPath, _copyFolderName));
        }
    }

    [Serializable]
    public class ExtensionFilterSerializable
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string[] Extensions { get; private set; }
    }
}