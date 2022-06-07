﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public bool pauseTime = false;
    public UnityEvent OnEnabled;

    void OnEnable()
    {
        if (pauseTime) Time.timeScale = 0;
        OnEnabled.Invoke();
        GetComponentInChildren<Button>().Select();
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadActiveScene()
    {
        SceneManager.LoadScene(SaveSystem.data.unlockedLevels);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
    public void ResetData()
    {
        SaveSystem.Reset();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
