using UnityEngine;
using System.Collections;

public class Turret : Controller
{

    public Transform player;
    Vector2 lastDirection;
    public float speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        Vector2 direction = player.position - transform.position;
        aimDirection = (Vector2.Lerp(direction, lastDirection, Time.deltaTime/speed));
        aimDirection.Normalize();
        shoot = true;
        lastDirection = direction;
    }
}
