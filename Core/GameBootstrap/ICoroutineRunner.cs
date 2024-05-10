using System.Collections;
using UnityEngine;

namespace Core.GameBootstrap
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}