using UnityEngine;
using System.Collections;

public abstract class Controller : MonoBehaviour {

    [HideInInspector]
    public Vector2 arrows=Vector2.zero;
    [HideInInspector]
    public bool jump=false;
    [HideInInspector]
    public bool shoot=false;
    [HideInInspector]
    public Vector2 aimDirection=Vector2.right;

}
