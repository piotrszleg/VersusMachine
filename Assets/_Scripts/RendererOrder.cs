using UnityEngine;
using System.Collections;

public class RendererOrder : MonoBehaviour {

    public int order = 0;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        rend.sortingOrder = order;
	}
}
