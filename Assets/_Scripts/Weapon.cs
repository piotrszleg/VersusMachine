using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public string weaponName="New Weapon";
    public TypeOfWeaopn type;
    public Sprite sprite;
    public Transform bullet;
    public AudioClip sound;
    public int damagePerShot = 10;
    public bool isAutomatic = true;
    public float rateOfFire = 2f;
}
public enum TypeOfWeaopn { Melee, Gun };