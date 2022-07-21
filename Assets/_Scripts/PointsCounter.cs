using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour {

    public int points=3;

	// Use this for initialization
	void Start () {
	
	}

    public void Add(int amount)
    {
        points--;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
