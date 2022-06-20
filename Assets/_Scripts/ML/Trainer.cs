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
    List<GameObject> fighters;
    System.Random random;
    SimpleMultiAgentGroup group1;
    SimpleMultiAgentGroup group2;
    public int team1Size;
    public int team2Size; 
    int current1size, current2size;
    int counter = 0;
    public int actionLimimt = 2000;

    void Start()
    {
        random = new System.Random();
        group1 = new SimpleMultiAgentGroup();
        group2 = new SimpleMultiAgentGroup();
        fighters = new List<GameObject>();
        Respawn();
    }

    public void CountAction()
    {
        ++counter;
        if(counter >= actionLimimt)
        {
            counter = 0;
            group1.GroupEpisodeInterrupted();
            group2.GroupEpisodeInterrupted();
            Respawn();
        }
    }

    public void Respawn()
    {
        counter = 0;
        foreach (GameObject fighter in fighters)
        {
            if (fighter != null)
            {
                fighter.GetComponent<ShooterActor>().StopAllCoroutines();
                Destroy(fighter);
            }
        }
        for(int i = 0; i<team1Size; i++)
        {
            GameObject tmp = Instantiate(myPrefab1, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
            tmp.transform.parent = gameObject.transform;
            ShooterActor newActor = tmp.GetComponent<ShooterActor>();
            newActor.setTrainer(gameObject);
            group1.RegisterAgent(tmp.GetComponent<ShooterActor>());
            fighters.Add(tmp);
        }
        current1size = team1Size;
        for (int i = 0; i < team2Size; i++)
        {
            GameObject tmp = Instantiate(myPrefab2, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
            tmp.transform.parent = gameObject.transform;
            ShooterActor newActor = tmp.GetComponent<ShooterActor>();
            newActor.setTrainer(gameObject);
            group2.RegisterAgent(tmp.GetComponent<ShooterActor>());
            fighters.Add(tmp);
        }
        current2size = team2Size;
    }

    public void Died1()
    {
        group2.AddGroupReward(1f / team1Size);
        group1.AddGroupReward(-1f / team1Size);
        current1size--;
        if (current1size <= 0)
        {
            group1.EndGroupEpisode();
            group2.EndGroupEpisode();
            Respawn();
        }
    }

    public void Died2()
    {
        group1.AddGroupReward(1f / team2Size);
        group2.AddGroupReward(-1f / team2Size);
        current2size--;
        if (current2size <= 0)
        {
            group1.EndGroupEpisode();
            group2.EndGroupEpisode();
            Respawn();
        }
    }
}