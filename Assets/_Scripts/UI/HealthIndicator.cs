using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour {

    public Killable target;
    Image img;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        img.fillAmount = (float)target.CurrentHP/(float)target.hp;
	}
}
