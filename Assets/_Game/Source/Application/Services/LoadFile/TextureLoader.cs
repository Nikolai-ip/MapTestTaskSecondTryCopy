using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Game.Source.Application.Services.LoadFile
{
    public class TextureLoader
    { 
        public async UniTask<LoadTextureResult> LoadTextureAsync(string path, CancellationToken token = default)
        {
            if (!File.Exists(path)) return new LoadTextureResult() { IsSuccess = false };
            using (UnityWebRequest loader = UnityWebRequestTexture.GetTexture("file://" + path))
            {
                await loader.SendWebRequest().ToUniTask(cancellationToken: token);

                if (loader.result == UnityWebRequest.Result.Success)
                {
                    var texture = DownloadHandlerTexture.GetContent(loader);
                    return new LoadTextureResult() {IsSuccess = true, Texture = texture};
                }

                return new LoadTextureResult() { IsSuccess = false };
            }
        }
    }

    public struct LoadTextureResult
    {
        public bool IsSuccess { get; set; }
        public Texture2D Texture { get; set; }

    }
}