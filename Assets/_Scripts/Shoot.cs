using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    Controller contr;
    private float nextFire = 0.0F;
    public float bulletKillTime = 100;
    public float bulletSpeed = 1;
    public bool faceMouse;
    [HideInInspector]
    public Vector2 shootDirection;

    public Weapon weapon;
    public PickableItem itemPrefab;

    public bool aim;
    public Transform hand;
    public SpriteRenderer weaponRenderer;

    public enum EquipMethod { Touch, TouchAndButton, TouchAndClick };
    public EquipMethod equipMetod;
    public int mouseButtonIndex = 0;
    public string buttonName = "e";
    public GameObject selectedObject;

    // Use this for initialization
    void Start () {
        contr = GetComponent<Controller>();
        if (GetComponent<HumanController>() != null)
        {
            if (SaveSystem.data.weapon != null) weapon = SaveSystem.data.weapon;
        }
        weaponRenderer.sprite = weapon.sprite;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (Time.timeScale == 0) return;

        shootDirection = contr.aimDirection;
        
        
        if (faceMouse)//Flip player to shooting direction.
        {
            if ((shootDirection.x > 0 && transform.localScale.x < 0) ||
                   (shootDirection.x < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        if (!aim)
        {
            if (transform.localScale.x < 0)
            {
                shootDirection = Vector2.left;
            }
            else
            {
                shootDirection = Vector2.right;
            }
        }
        if (aim)
        {
            hand.right = (Vector2)contr.aimDirection;
            if (transform.localScale.x < 0)
            {
                hand.right = new Vector2(-hand.right.x, -hand.right.y);
            }
        }
        if (contr.shoot && Time.time > nextFire && weapon.type == TypeOfWeaopn.Gun)
        {
            nextFire = Time.time + 1 / weapon.rateOfFire;
            ShootBullet();
            if (!weapon.isAutomatic) contr.shoot = false;
        }
        if (selectedObject != null)
        {
            switch (equipMetod)
            {
                case EquipMethod.Touch:
                    selectedObject.SendMessage("Interact", gameObject, SendMessageOptions.DontRequireReceiver);
                    break;
                case EquipMethod.TouchAndButton:
                    if (Input.GetKeyDown(buttonName))
                    {
                        selectedObject.SendMessage("Interact", gameObject, SendMessageOptions.DontRequireReceiver);
                    }
                    break;
                case EquipMethod.TouchAndClick:
                    if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedObject.transform.position) < 1 && Input.GetMouseButtonDown(mouseButtonIndex))
                    {
                        selectedObject.SendMessage("Interact", gameObject, SendMessageOptions.DontRequireReceiver);

                    }
                    break;
            }
        }
    }

    void ShootBullet()
    {
        Vector2 bulletPosition;
        bulletPosition = (Vector2)hand.position+shootDirection*1.5f;
        
        //Quaternion bulletRotation = Quaternion.AngleAxis(Vector2.Angle(transform.right, shootDirection), transform.forward);
        Transform newBullet = (Transform)Instantiate(weapon.bullet, bulletPosition, Quaternion.identity);
        newBullet.transform.right = shootDirection;
        if (transform.localScale.x < 0)
        {
            newBullet.transform.localScale = new Vector3(-newBullet.transform.localScale.x, newBullet.transform.localScale.y, newBullet.transform.localScale.z);
            newBullet.GetComponent<Effector>().value = weapon.damagePerShot;
        }
        if (weapon.sound != null) AudioManager.Play(weapon.sound);
        Destroy(newBullet.gameObject, bulletKillTime);
    }

    public void Equip(Weapon newWeapon) {
            PickableItem dropped = (PickableItem)Instantiate(itemPrefab, transform.position, Quaternion.identity);
            dropped.weapon = weapon;
            if(equipMetod==EquipMethod.Touch)dropped.droppedBy = transform;
            weapon = newWeapon;
            weaponRenderer.sprite = newWeapon.sprite;
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject!=gameObject && other.gameObject.layer==10 && contr.shoot && Time.time > nextFire && weapon.type == TypeOfWeaopn.Melee)
        {
            if (weapon.sound != null) AudioManager.Play(weapon.sound);
            nextFire = Time.time + 1 / weapon.rateOfFire;
            other.gameObject.SendMessage("Damage", weapon.damagePerShot, SendMessageOptions.DontRequireReceiver);
        }
    }
    void OnDestroy()
    {
        SaveSystem.data.weapon = weapon;
    }
}
