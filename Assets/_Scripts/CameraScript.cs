using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    public TMP_Text healthBar;
    public TMP_Text ResultScene;
    public Button nextLevelButton;
    public Button restartButton;
    public Killable player;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.text = $"Health: {player.CurrentHP}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
