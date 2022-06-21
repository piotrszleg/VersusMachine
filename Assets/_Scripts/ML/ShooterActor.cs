using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.Events;
using System;

public class ShooterActor : Agent
{
    Controller controller;
    public GameObject trainer;
    float curDistance;
    float distance;
    public float deathReward = -0.1f;
    public float damageReward = 0.1f;
    public float killReward = 1.0f;
    public float existenceReward = 0f;
    public float missReward = 0f;
    public float fallReward = -1f;
    public string targetTag = "Player";
    int counter = 0;
    Vector2 direction;


    public override void Initialize()
    {
        base.Initialize();
        controller = GetComponent<Controller>();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        
    }
    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        //distance = Mathf.Infinity;
        //foreach (Transform child in transform.parent.transform)
        //    if (child.gameObject.tag == targetTag && !ReferenceEquals(gameObject, child.gameObject))
        //    {
        //        curDistance = (child.transform.localPosition - transform.localPosition).sqrMagnitude;
        //        if (curDistance < distance)
        //        {
        //            distance = curDistance;
        //            direction = (child.transform.localPosition - transform.localPosition).normalized;
        //        }
        //    }
        // TO DO: Popracowa? nad parametrami funkcji
        //trainer.GetComponent<Trainer>().CountAction();
        controller.jump = Convert.ToBoolean(vectorAction.DiscreteActions[0]);
        controller.arrows.x = Mathf.Clamp(vectorAction.ContinuousActions[0], -1, 1);
        controller.shoot = Convert.ToBoolean(vectorAction.DiscreteActions[1]);
        controller.aimDirection = new Vector2(Mathf.Cos(vectorAction.ContinuousActions[1] * 360 * Mathf.Deg2Rad), Mathf.Sin(vectorAction.ContinuousActions[1] * 360 * Mathf.Deg2Rad));
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetButton("Jump") ? 1 : 0;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        discreteActionsOut[1] = Input.GetButton("Fire1") ? 1 : 0;
        Vector3 aimDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Vector2.Angle(new Vector2(aimDirection.x, aimDirection.y), Vector2.right);
        if (aimDirection.y < 0f)
        {
            angle = 360 - angle;
        }
        continuousActionsOut[1] = angle / 360;
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public void setTrainer(GameObject trainerObject)
    {
        trainer = trainerObject;
    }

    public void Killing()
    {
        
        AddReward(killReward);
    }

    public void DealingDamage()
    {
        AddReward(damageReward);
    }

    public void MissingAttack()
    {

    }

    public void Dying()
    {
        AddReward(deathReward);
        EndEpisode();
        if (trainer != null)
        {
            var tr = trainer.GetComponent<Trainer>();
            if (tr != null)
                if (tag == "Player")
                    tr.Died1();
                else
                    tr.Died2();
        }
    }

    public void Falling()
    {
        AddReward(fallReward);
        EndEpisode();
        if (trainer != null)
        {
            var tr = trainer.GetComponent<Trainer>();
            if (tr != null)
                if (tag == "Player")
                    tr.Died1();
                else
                    tr.Died2();
        }
        
    }
}