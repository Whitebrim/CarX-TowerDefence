using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Services.AssetManagement
{
    public class AddressablesProvider : IAssetProvider
    {
        public TObject Load<TObject>(string path) where TObject : Object => Addressables.LoadAssetAsync<TObject>(path).WaitForCompletion();
    }
}