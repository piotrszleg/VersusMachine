using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Killable : MonoBehaviour
{

    public int hp = 100;
    private int currentHP;
    private bool alive = true;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
    }
    public Transform corpsePrefab;
    public AudioClip hurt;
    public UnityEvent onDeath;

    void Start()
    {
        currentHP = hp;
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            ShooterActor actor = gameObject.GetComponent<ShooterActor>();
            if (actor != null)
                actor.Falling();
            if(tag == "Player")
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            alive = false;
            Destroy(gameObject);
            //Damage((100, null));
        }
    }

    void Damage((int, GameObject) info)
    {
        int amount = info.Item1;
        GameObject enemy = info.Item2;

        if (enemy != null)
        {
            ShooterActor actor = enemy.GetComponent<ShooterActor>();
            if (actor != null) actor.DealingDamage();
            else actor.MissingAttack();

            if (!ReferenceEquals(gameObject, enemy) && enemy.tag != tag)
            {
                currentHP -= amount;
                if (currentHP <= 0 && alive)
                {
                    alive = false;
                    if (hurt != null) AudioManager.Play(hurt);
                    if (corpsePrefab != null)
                    {
                        Transform corpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity) as Transform;
                        if (transform.localScale.x < 0)
                        {
                            corpse.localScale = new Vector2(-corpse.localScale.x, corpse.localScale.y);
                        }
                    }
                    onDeath.Invoke();
                    if (enemy != null)
                    {
                        actor = enemy.GetComponent<ShooterActor>();
                        if (actor != null) actor.Killing();
                    }
                    ShooterActor victim = gameObject.GetComponent<ShooterActor>();
                    if (victim != null) victim.Dying();
                    if (tag == "Player")
                    {
                        Scene scene = SceneManager.GetActiveScene();
                        SceneManager.LoadScene(scene.name);
                    }
                    Destroy(gameObject);
                }
            }
        }

    }

    void Heal(int amount)
    {
        if (currentHP + amount < hp)
        {
            currentHP += amount;
        }
        else
        {
            currentHP = hp;
        }
    }

    public bool IsAlive()
    {
        return alive;
    }
}