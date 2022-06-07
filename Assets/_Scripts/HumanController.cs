using UnityEngine;
using System.Collections;

public class HumanController : Controller {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        jump = Input.GetButton("Jump");
        arrows.x = Input.GetAxis("Horizontal");
        arrows.y= Input.GetAxis("Vertical");
        shoot = Input.GetButton("Fire1");
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (cameraPosition - transform.position);
        aimDirection.Normalize();
    }
}
