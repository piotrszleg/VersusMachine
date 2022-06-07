using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {

    public UnityEvent onEnter;
    public UnityEvent onExit;
    Collider2D touchingCollider;
    bool state = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (touchingCollider == null)
        {
            onEnter.Invoke();
            touchingCollider = other;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other == touchingCollider)
        {
            onExit.Invoke();
            touchingCollider = null;
        }
    }
}
