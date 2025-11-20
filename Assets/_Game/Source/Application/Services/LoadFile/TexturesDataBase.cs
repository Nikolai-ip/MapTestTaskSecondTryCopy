using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Source.Application.Services.LoadFile
{
    public class TexturesDataBase
    {
        private readonly IFileLoader _fileLoader;
        private readonly TextureLoader _textureLoader = new();
        private readonly Dictionary<string, Texture2D> _loadedTextures = new();

        public TexturesDataBase(IFileLoader fileLoader)
        {
            _fileLoader = fileLoader;
        }

        public async UniTask<LoadTextureResult> TryGetTexture(string imageName)
        {
            if (_loadedTextures.TryGetValue(imageName, out var texture))
            {
                return new LoadTextureResult() { IsSuccess = true, Texture = texture };
            }

            LoadTextureResult result =
                await _textureLoader.LoadTextureAsync(Path.Combine(_fileLoader.DirectoryCopyPath, imageName));

            if (result.IsSuccess)
            {
                if (!_loadedTextures.TryAdd(imageName, result.Texture))
                    Debug.LogWarning($"Texture {imageName} has already been loaded");
            }
            return result;
        }
        public async UniTask<string> LoadImageInToProject()
        {
            string imageName = _fileLoader.LoadFile();
            return imageName;
        }
    }
}