using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public PointsCounter counter;
    int displayedPoints;
    Image[] children;
    public float wait=0.2f;

    // Use this for initialization
    void Start()
    {
        children = GetComponentsInChildren<Image>();
        StartCoroutine(UpdateImages());
    }

    IEnumerator UpdateImages()
    {
        displayedPoints = counter.points;
        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled = false;
        }
        for (int i = 0; i < displayedPoints; i++)
        {
            children[i].enabled = true;
            yield return new WaitForSeconds(wait);
        }
    }
}
