using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.MLAgents;

public class Trainer : MonoBehaviour
{
    public GameObject myPrefab1;
    public GameObject myPrefab2;
    public List<GameObject> spawners;
    public List<GameObject> fighters;
    System.Random random;
    public int team1Size;
    public int team2Size;
    public int actionLimimt = 3000;

    void Start()
    {
        random = new System.Random();
    }

    public void Died1()
    {
        SetUp(Instantiate(myPrefab1, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity));
    }

    public void Died2()
    {
        SetUp(Instantiate(myPrefab2, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity));
    }

    private void SetUp(GameObject tmp)
    {
        tmp.transform.parent = gameObject.transform;
        ShooterActor newActor = tmp.GetComponent<ShooterActor>();
        newActor.setTrainer(gameObject);
    }
}