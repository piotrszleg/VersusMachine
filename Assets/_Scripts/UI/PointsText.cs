using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PointsText : MonoBehaviour {

    public PointsCounter counter;
    public string before;
    Text uiText;
    public string after=" points";
    public List<Killable> actors;

        // Use this for initialization
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

        // Update is called once per frame
        void Update () {
                uiText.text = "Left: " + GetAlive().ToString();
        }
}
