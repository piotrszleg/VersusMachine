using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PointsText : MonoBehaviour {
    public PointsCounter counter;
    public string before;
    Text uiText;
    public string after=" points";
    public List<Killable> actors;
    void Start () {
            uiText = GetComponent<Text>();
    }

    int GetAlive()
    {
        int i = 0;
        foreach (Killable a in actors)
        {
            if (a.IsAlive())
            {
                    i++;
            }
        }
        return i;
    }

    void Update ()
    {
        int alive = GetAlive();
        uiText.text = "Left: " + alive.ToString();
        if (alive <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
