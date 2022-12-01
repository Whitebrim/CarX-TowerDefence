using System;
using Core.Infrastructure.Services;

namespace Core.Services
{
    public interface ISceneLoader : IService
    {
        void Load(string sceneName, Action onLoad = null);
    }
}