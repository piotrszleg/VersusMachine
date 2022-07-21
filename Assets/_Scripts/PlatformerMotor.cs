using UnityEngine;
using System.Collections;

public class PlatformerMotor : MonoBehaviour {

    public float jumpPower = 750f;
    public float speed = 6f;
    public float airControl = 0.1f;
    public bool canMove=true;
    public bool canJump=true;
    public bool canWallJump = true;
    public bool faceDirection=true;
    public bool jumping=false;

    Controller contr;
    Rigidbody2D body;
    Animator anim;
    Shoot shooter;
    [System.Serializable]
    public struct RayDistances
    {
        public float down;
        public float up;
        public float left;
        public float right;
    }
    public RayDistances distances;
    public float touchDistance = 0.1f;
    [System.Serializable]
    public struct Touch
    {
        public bool ground;
        public bool ceiling;
        public bool left;
        public bool right;
    }
    public Touch touch;
    const int mask = ~(1 << 3 | 1 << 10 | 1 << 9);

    void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        contr = GetComponent<Controller>();
        Shoot checkShooter = GetComponent<Shoot>();
        if (checkShooter.faceMouse)
        {
            shooter = checkShooter;
        }
    }

    void Update () {
        UpdateCollision();
            if (!canMove && !canJump)
            {
                anim.SetInteger("state", 0);
                body.drag = 100000;
            body.velocity = Vector2.zero;
            }else
            {
                body.drag = 0;
            }
            if (canMove) UpdateMovement(contr.arrows.x);
            if (canJump && (contr.jump || contr.arrows.y>0)) Jump();
            if (canWallJump) UpdateWallJump();
        
    }

    public void Stop()
    {
        canMove = false;
        canJump = false;
        canWallJump = false;
    }

    void UpdateCollision()
    {
        RaycastHit2D downHit = Physics2D.Raycast(transform.position+Vector3.down*distances.down, Vector2.down, 1, mask);
        if (downHit.point!=Vector2.zero)
        {
            Debug.DrawLine(transform.position + Vector3.down * distances.down, downHit.point);
        }
        if (downHit && downHit.distance<touchDistance)
        {
            touch.ground = true;
        }
        else
        {
            touch.ground = false;
        }
        RaycastHit2D upHit = Physics2D.Raycast(transform.position + Vector3.up * distances.up, Vector2.up, 1, mask);
        if (upHit.point != Vector2.zero)
        {
            Debug.DrawLine(transform.position + Vector3.up * distances.up, upHit.point);
        }
        if (upHit && upHit.distance < touchDistance)
        {
            touch.ceiling = true;
        }
        else
        {
            touch.ceiling = false;
        }
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position + Vector3.left * distances.left, Vector2.left, 1, mask);
        if (leftHit.point != Vector2.zero)
        {
            Debug.DrawLine(transform.position + Vector3.left * distances.left, leftHit.point);
        }
        if (leftHit && leftHit.distance < touchDistance)
        {
            touch.left = true;
        }
        else
        {
            touch.left = false;
        }
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.right * distances.right, Vector2.right, 1, mask);
        if (rightHit.point != Vector2.zero)
        {
            Debug.DrawLine(transform.position + Vector3.right * distances.right, rightHit.point);
        }
        if (rightHit && rightHit.distance < touchDistance)
        {
            touch.right = true;
        }
        else
        {
            touch.right = false;
        }
    }

    public void UpdateMovement(float xAxis)
    {
        if (
            (!touch.left && !touch.right)
            || (xAxis < 0 && touch.right)
            || (xAxis > 0 && touch.left)
            )
            {
            if (anim != null)
            {
                if (xAxis != 0)
                {
                    anim.SetInteger("state", 1);
                }
                else
                {
                    anim.SetInteger("state", 0);
                }
            }
            if (touch.ground)
            {
                body.velocity = new Vector2(xAxis * speed, body.velocity.y);
            }
            else if(xAxis!=0)
            {
                body.velocity = new Vector2(xAxis*speed*airControl + body.velocity.x*(1-airControl), body.velocity.y);
            }
            if (((xAxis < 0 && transform.localScale.x>0)
            || (xAxis > 0 && transform.localScale.x<0))&&faceDirection) {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void Jump()
    {
        if (touch.ground && !jumping)
        {
            touch.ground = false;
            jumping = true;
            body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void UpdateWallJump()
    {
        if ((touch.left || touch.right) && !touch.ground && (Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            anim.SetInteger("state", 2);
            //if (contr.arrows.x < 0 && touch.right && jumping){Debug.Log(0);}
            if (contr.arrows.x < 0 && touch.right && !jumping)
            {
                body.drag = 0;
                body.AddForce(new Vector2(-1, 1) * jumpPower, ForceMode2D.Impulse);
                touch.right = false;
                jumping = true;
            }
            else if (contr.arrows.x > 0 && touch.left && !jumping)
            {
                body.drag = 0;
                body.AddForce(new Vector2(1, 1) * jumpPower, ForceMode2D.Impulse);
                touch.left = false;
                jumping = true;
            }
            else
            {
                if (shooter != null)
                {
                    shooter.faceMouse = false;
                }
                body.drag = 300;
                if (((touch.right && transform.localScale.x > 0)
                || (touch.left && transform.localScale.x < 0)))
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            if (shooter != null)
            {
                shooter.faceMouse = true;
            }
            body.drag = 0;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        jumping = false;
    }
    /*void OnCollisionEnter2D (Collision2D col)
    {
        foreach (ContactPoint2D contact in col.contacts)
        {
            //Debug.Log("Enter "+  contact.normal);
            if (contact.normal.y > 0)
            {
                touch.ground = true;
            }
            if (contact.normal.y < 0)
            {
                touch.ceiling = true;
            }
            if (contact.normal.x>0)
            {
                touch.right = true;
            }
            if (contact.normal.x < 0)
            {
                touch.left = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        foreach (ContactPoint2D contact in col.contacts)
        {
            //Debug.Log("Exit" + contact.normal);
            if (contact.normal.y > 0)
            {
                touch.ground = false;
            }
            if (contact.normal.y < 0)
            {
                touch.ceiling = false;
            }
            if (contact.normal.x < 0)
            {
                touch.right = false;
            }
            if (contact.normal.x > 0)
            {
                touch.right = false;
            }
        }
    }*/
}
