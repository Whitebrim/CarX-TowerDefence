using System.Collections;
using Core.Infrastructure.Services;
using UnityEngine;

public interface ICoroutineRunner : IService
{
    Coroutine StartCoroutine(IEnumerator coroutine);
}