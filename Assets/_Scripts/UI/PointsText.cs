using UnityEngine;
using UnityEngine.UI;

public class PointsText : MonoBehaviour {

    public PointsCounter counter;
    public string before;
    Text uiText;
    public string after=" points";

	// Use this for initialization
	void Start () {
        uiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        uiText.text = before+counter.points.ToString()+after;
	}
}
