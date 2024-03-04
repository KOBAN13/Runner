using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour, IDisposable
{
    private event Action LoadSceneFinish;
    private void AddListener() => LoadSceneFinish += LoadScene;

    public void InvokeEvent()
    {
        AddListener();
        LoadSceneFinish?.Invoke();
    }

    public void Dispose() => LoadSceneFinish -= LoadScene;

    private void LoadScene() => SceneManager.LoadScene("Finish");
}
