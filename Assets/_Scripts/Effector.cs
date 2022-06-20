using UnityEngine;
using System.Collections;

public class Effector : MonoBehaviour
{

    public string methodName = "";
    public int value;
    public bool destroyAfterEffect = true;
    public float destroyTime = 0;
    public bool onlyCharacters = false;
    public GameObject sender = null;

    void OnCollisionEnter2D(Collision2D col)
    {
        OnCollision(col.collider);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        OnCollision(col);
    }

    void OnCollision(Collider2D col)
    {
        if (!onlyCharacters || col.gameObject.layer == 10)
        {
            col.gameObject.SendMessage(methodName, (value, sender), SendMessageOptions.DontRequireReceiver);
            //Debug.Log("Applied " + methodName + "(" + value + ")"+" to "+col.gameObject.name);
        }
        if (destroyAfterEffect) Destroy(gameObject, destroyTime);
    }
}