using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Killable : MonoBehaviour {

    public int hp = 100;
    private int currentHP;
    public int CurrentHP {
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
        if (transform.position.y < 0)
        {
            Damage(100);
        }
    }

    void Damage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            if (hurt != null) AudioManager.Play(hurt);
            if (corpsePrefab != null)
            {
                Transform corpse = (Instantiate(corpsePrefab, transform.position, Quaternion.identity) as Transform);
                if (transform.localScale.x < 0)
                {
                    corpse.localScale = new Vector2(-corpse.localScale.x, corpse.localScale.y);
                }
            }
            onDeath.Invoke();
            Destroy(gameObject);
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
}
