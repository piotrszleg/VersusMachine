using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class TextDrawer : MonoBehaviour
{

    string[] toDisplay;
    Text display;
    public float speed = 10f;
    public float forgetTime = 0.5f;
    public PlatformerMotor player;
    public bool stopPlayer;
    Action finishedAction;
    bool drawing = false;

    // Use this for initialization
    void Start()
    {
        display = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Say(string text)
    {
        toDisplay = new string[] { text };
        StartCoroutine(DisplayText(toDisplay));
        finishedAction = null;
    }
    public void Say(string[] text)
    {
        toDisplay = text;
        StartCoroutine(DisplayText(toDisplay));
        finishedAction = null;
    }
    public void Say(string[] text, Action onFinished)
    {
        toDisplay = text;
        finishedAction = onFinished;
    }

    IEnumerator DisplayText(string[] text)
    {
        if (drawing)
        {
            yield break;
        }
        drawing = true;
        if (player != null && stopPlayer)
        {
            player.canMove = false;
            player.canJump = false;
        }
        for (int i=0; i < text.Length; i++)
        {
            display.text = "";
            for (int j = 0; j < text[i].Length; j++)
            {
                if (Input.GetButton("Jump") && stopPlayer)
                {
                    display.text = text[i];
                    break;
                }
                display.text += text[i][j];
                yield return new WaitForSeconds(1/speed);
            }
            yield return new WaitForSeconds(forgetTime);
        }
        display.text = "";
        if (player != null && stopPlayer)
        {
            player.canMove = true;
            player.canJump = true;
        }
        if (finishedAction != null)
        {
            finishedAction.Invoke();
        }
        drawing = false;
    }
}