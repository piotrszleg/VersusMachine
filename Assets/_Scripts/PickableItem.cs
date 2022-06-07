using UnityEngine;
using System.Collections;

public class PickableItem : MonoBehaviour {

    public Weapon weapon;
    public Transform droppedBy;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = weapon.sprite;
        if (weapon.sprite != null)
        {
            Vector2 S = weapon.sprite.bounds.size;
            BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
            BoxCollider2D trigger = gameObject.GetComponentsInChildren<BoxCollider2D>()[1];
            collider.size = trigger.size = S;
            collider.offset = trigger.offset = weapon.sprite.bounds.center;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(droppedBy!=null&&Vector2.Distance(transform.position, droppedBy.position) > 1)
        {
            droppedBy = null;
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (droppedBy==null)
        {
            Shoot arcShooter = other.GetComponent<Shoot>();
            if (arcShooter != null)
            {
                arcShooter.selectedObject = gameObject;
            }
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
    }
    void Interact(GameObject caller)
    {
        caller.SendMessage("Equip", weapon, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
