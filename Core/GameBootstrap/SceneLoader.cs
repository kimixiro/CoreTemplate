using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameBootstrap
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action<bool> onCompleted = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                onCompleted?.Invoke(false);
                return;
            }

            _coroutineRunner.StartCoroutine(LoadScene(name, onCompleted));
        }

        private IEnumerator LoadScene(string nextScene, Action<bool> onCompleted)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onCompleted?.Invoke(true);
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            if (waitNextScene == null)
            {
                onCompleted?.Invoke(false);
                yield break;
            }

            while (!waitNextScene.isDone)
                yield return null;

            onCompleted?.Invoke(true);
        }
    }
}