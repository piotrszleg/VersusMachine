using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trainer : MonoBehaviour
{
    public GameObject myPrefab;
    public List<GameObject> spawners;
    System.Random random;

    void Start()
    {
        random = new System.Random();
    }

    public void NewLocation(GameObject gameObject)
    {
        gameObject.transform.localPosition = spawners[random.Next(spawners.Count)].transform.localPosition;
    }

    public void Respawn()
    {
        GameObject tmp = Instantiate(myPrefab, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
        tmp.transform.parent = gameObject.transform;
        ShooterActor newActor = tmp.GetComponent<ShooterActor>();
        newActor.setTrainer(gameObject);
    }
}
