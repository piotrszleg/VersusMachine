using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour {

    public TextDrawer textDrawer;
    public string[] message;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Shoot shooter = other.GetComponent<Shoot>();
        if (shooter != null)
        {
            shooter.selectedObject = gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Shoot shooter = other.GetComponent<Shoot>();
        if (shooter != null)
        {
            if (shooter.selectedObject == gameObject)
            {
                shooter.selectedObject = null;
            }
        }
    }
    public void Interact(GameObject caller)
    {
        if (caller.tag == "Player")
        {
            textDrawer.Say(message);
        }
    }
}
