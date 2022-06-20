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
    List<ShooterActor> group1;
    List<ShooterActor> group2;
    public int team1Size;
    public int team2Size;
    int current1size, current2size;
    int counter = 0;
    public int actionLimimt = 3000;

    void Start()
    {
        random = new System.Random();
        group1 = new List<ShooterActor>();
        group2 = new List<ShooterActor>();
        //foreach (GameObject fighter in fighters)
        //{
        //    if(fighter.tag == "Player")
        //    {
        //        group1.Add(fighter.GetComponent<ShooterActor>());
        //    }
        //    else
        //    {
        //        group2.Add(fighter.GetComponent<ShooterActor>());
        //    }
        //}
        current1size = team1Size;
        current2size = team2Size;
        //Respawn();
    }

    //public void CountAction()
    //{
    //    ++counter;
    //    if(counter >= actionLimimt)
    //    {
    //        counter = 0;
    //        //group1.GroupEpisodeInterrupted();
    //        //group2.GroupEpisodeInterrupted();
    //        Respawn();
    //    }
    //}

    //public void Respawn()
    //{
    //    counter = 0;
    //    foreach (GameObject fighter in fighters)
    //    {
    //        if (fighter != null)
    //        {
    //            Destroy(fighter);
    //        }
    //    }
    //    fighters.Clear();
    //    group1.Clear();
    //    for(int i = 0; i<team1Size; i++)
    //    {
    //        GameObject tmp = Instantiate(myPrefab1, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
    //        tmp.transform.parent = gameObject.transform;
    //        ShooterActor newActor = tmp.GetComponent<ShooterActor>();
    //        newActor.setTrainer(gameObject);
    //        group1.Add(tmp.GetComponent<ShooterActor>());
    //        fighters.Add(tmp);
    //    }
    //    current1size = team1Size;
    //    group2.Clear();
    //    for (int i = 0; i < team2Size; i++)
    //    {
    //        GameObject tmp = Instantiate(myPrefab2, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
    //        tmp.transform.parent = gameObject.transform;
    //        ShooterActor newActor = tmp.GetComponent<ShooterActor>();
    //        newActor.setTrainer(gameObject);
    //        group2.Add(tmp.GetComponent<ShooterActor>());
    //        fighters.Add(tmp);
    //    }
    //    current2size = team2Size;
    //}

    public void Died1()
    {
        GameObject tmp = Instantiate(myPrefab1, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
        tmp.transform.parent = gameObject.transform;
        ShooterActor newActor = tmp.GetComponent<ShooterActor>();
        newActor.setTrainer(gameObject);
        ////group2.AddGroupReward(1f / team1Size);
        ////group1.AddGroupReward(-1f / team1Size);
        //current1size--;
        //if (current1size <= 0)
        //{
        //    foreach (ShooterActor actor in group1)
        //    {
        //        if (actor != null)
        //        {
        //            actor.EndEpisode();
        //        }
        //    }
        //    foreach (ShooterActor actor in group2)
        //    {
        //        if (actor != null)
        //        {
        //            actor.EndEpisode();
        //        }
        //    }
        //    //group1.EndGroupEpisode();
        //    //group2.EndGroupEpisode();
        //    Respawn();
        //}
    }

    public void Died2()
    {
        GameObject tmp = Instantiate(myPrefab2, spawners[random.Next(spawners.Count)].transform.position, Quaternion.identity);
        tmp.transform.parent = gameObject.transform;
        ShooterActor newActor = tmp.GetComponent<ShooterActor>();
        newActor.setTrainer(gameObject);
        ////group1.AddGroupReward(1f / team2Size);
        ////group2.AddGroupReward(-1f / team2Size);
        //current2size--;
        //if (current2size <= 0)
        //{
        //    foreach (ShooterActor actor in group1)
        //    {
        //        if (actor != null)
        //        {
        //            actor.EndEpisode();
        //        }
        //    }
        //    foreach (ShooterActor actor in group2)
        //    {
        //        if (actor != null)
        //        {
        //            actor.EndEpisode();
        //        }
        //    }
        //    Respawn();
        //}
    }
}