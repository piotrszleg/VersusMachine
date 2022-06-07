using UnityEngine;
using System.Collections;

public class PlatformerEnemyController : Controller {

    PlatformerMotor motor;
    public int dir = 1;
    public Vector2 cliffRayOffset=new Vector2(0.75f, -0.5f);
    public Transform player;
    public Vector2 playerDetectorOffset = new Vector2(1.2f, 0);
    public float playerDetectorRange = 5f;
    public float playerDetectorAngle = 30f;

    public bool patrolling = true;
    public bool seePlayer = false;
    public float chasingTime = 20f;
    float toPatrol = 0f;
    float lastJump = 0f;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlatformerMotor>();
    }
	
	// Update is called once per frame
	void Update () {
        jump = false;
        if (player == null)
        {
            shoot = false;
            return;
        }
        bool lastCheck = seePlayer;
        if (dir == 0 && patrolling)
        {
            if (transform.localScale.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
        }
        Vector2 playerDetectorOrigin = (Vector2)transform.position + new Vector2(playerDetectorOffset.x * dir, playerDetectorOffset.y);

        if (Vector2.Angle(Vector2.right * dir, (Vector2)player.transform.position-playerDetectorOrigin) < playerDetectorAngle) {
            RaycastHit2D playerDetectorHit = Physics2D.Raycast(playerDetectorOrigin, player.transform.position - transform.position, playerDetectorRange);
            Debug.DrawRay(playerDetectorOrigin, (Vector2)player.transform.position - (Vector2)playerDetectorOrigin, Color.red);
            seePlayer = (playerDetectorHit.transform == player && playerDetectorHit);
        }
        else
        {
            seePlayer = false;
        }
        if(Vector2.Distance(transform.position, player.transform.position) < 1)
        {
            seePlayer=true;
        }
        if (seePlayer)
        {
            patrolling = false;
        }
        else if(lastCheck)
        {
            toPatrol = Time.time + chasingTime;
        }
        else if(toPatrol<=Time.time)
        {
            patrolling = true;
        }
        if (patrolling)
        {
            UpdatePatrol();
        }
        else
        {
            UpdateChase();
        }
        arrows.x = dir;
    }

    void UpdatePatrol()
    {
        shoot = false;
        Vector2 cliffRayOrigin = (Vector2)transform.position + new Vector2(cliffRayOffset.x * dir, cliffRayOffset.y);
        RaycastHit2D cliffhHit = Physics2D.Raycast(cliffRayOrigin, -Vector2.up, 10);
        Debug.DrawRay(cliffRayOrigin, -Vector2.up * cliffhHit.distance, Color.blue);
        if ((cliffhHit.distance > 2 || !cliffhHit) && motor.touch.ground)
        {
            dir *= -1;
        }
    }

    void UpdateChase()
    {
        if (seePlayer)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }
        Vector2 difference = transform.position - player.position;
        if (difference.x > 1 || (difference.x > 0 && transform.localScale.x>0))// && difference.y<1)
        {
            dir = -1;
        }
        else if ((difference.x < -1 || (difference.x<0&&transform.localScale.x<0)))//&& difference.y < 1)
        {
            dir = 1;
        }
        else
        {
            dir = 0;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player == null) return;
        if (patrolling&&motor.touch.ground)
        {
            dir *= -1;
        }
        else if(!patrolling&&other.transform!=player&&other.transform.parent!=player&&motor.touch.ground&&!seePlayer&&Time.time-lastJump>1)
        {
            jump=true;
            lastJump = Time.time;
            motor.touch.ground = false;
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}
