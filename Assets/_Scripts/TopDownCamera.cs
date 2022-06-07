using UnityEngine;
using System.Collections;

public class TopDownCamera : MonoBehaviour {

    public TopDownPlayer player;
    public float speed = 1;
    public bool followMouse = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            if (followMouse)
            {
                newPosition += (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized*2;
            }
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
        }
    }
}
