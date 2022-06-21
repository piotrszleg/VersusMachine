using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MusicScript : MonoBehaviour
{

    [SerializeField] private AudioSource audio;
    private bool isAudioPlaying = false;
    public Button button;
    public Sprite img1;
    public Sprite img2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAudioPlaying)
        {
            button.GetComponent<Image>().sprite = img2;
            
            audio.volume = 0.0f;
        }
        else
        {
            button.GetComponent<Image>().sprite = img1;
            audio.volume = 1.0f;
        }
    }

    public void Switch()
    {
        isAudioPlaying = !isAudioPlaying;
    }
}
