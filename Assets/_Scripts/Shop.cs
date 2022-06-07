using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject shopMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        Shoot arcShooter = other.GetComponent<Shoot>();
        if (arcShooter != null)
        {
            arcShooter.selectedObject = gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Shoot arcShooter = other.GetComponent<Shoot>();
        if (arcShooter != null)
        {
            if (arcShooter.selectedObject == gameObject)
            {
                arcShooter.selectedObject = null;
            }
        }
        shopMenu.SetActive(false);
    }
    public void Interact(GameObject caller)
    {
        if (caller.tag=="Player")
        {
            shopMenu.SetActive(true);/*
            other.transform.position = transform.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            */
        }
    }
}
