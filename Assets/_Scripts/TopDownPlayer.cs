using UnityEngine;
using System.Collections;

public class TopDownPlayer : MonoBehaviour {

    Rigidbody2D body;
    public float walkSpeed=4;
    public float runSpeed = 6;
    public enum Face {Nothing, Direction, Side};
    public Face face;
    public bool canMove=true;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (!canMove) return;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.sqrMagnitude > 0 && face==Face.Direction)
        {
            transform.right = input;
        }
        if (face == Face.Side)
        {
            if (((input.x<0 && transform.localScale.x > 0)
                || (input.x>0 && transform.localScale.x < 0)))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            body.velocity = input.normalized * runSpeed;
        }else
        {
            body.velocity = input.normalized * walkSpeed;
        }
	}
}
