using UnityEngine;
using UnityEngine.UI;

public class PointsImages : MonoBehaviour {

    public PointsCounter counter;
    int displayedPoints;
    Image[] children;

	// Use this for initialization
	void Start () {
        children = GetComponentsInChildren<Image>();
        UpdateImages();
    }
	
	// Update is called once per frame
	void Update () {
        if (counter.points != displayedPoints)
        {
            UpdateImages();
        }
	}

    void UpdateImages()
    {
        displayedPoints = counter.points;
        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled=i <= displayedPoints;
        }
    }
}
