using UnityEngine;
using System.Collections;

public class PlatformerCamera : MonoBehaviour {

    public PlatformerMotor player;
    public float speed=1;
    public bool checkIfGrounded = true;
    public Rect bounds = new Rect(0, 0, 64, 64);
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            Rect nBounds = new Rect(bounds.x + width / 2, bounds.y + height / 2, bounds.width - width, bounds.height - height);
            Vector3 newPosition = transform.position;
            newPosition.x = player.transform.position.x;
            if (player.touch.ground || !checkIfGrounded)
            {
                newPosition.y = player.transform.position.y;
            }
            newPosition = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
            if (!nBounds.Contains(transform.position))
            {
                transform.position = new Vector3(transform.position.x + bounds.x + 0.5f, newPosition.y, transform.position.z);
            }
            else if (nBounds.Contains(newPosition))
            {
                transform.position = newPosition;
            }
            else if (nBounds.Contains(new Vector3(newPosition.x, transform.position.y, transform.position.z)))
            {
                transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
            }
            else if (nBounds.Contains(new Vector3(transform.position.x, newPosition.y, transform.position.z)))
            {
                transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
            }
        }
    }
}
