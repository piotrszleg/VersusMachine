using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine.Events;

public class ShooterActor : Agent
{
    Controller controller;
    public GameObject trainer;
    float curDistance;
    float distance;
    public float deathReward = 0.0f;
    public float distanceReward = 0.1f;
    public float damageReward = 0.1f;
    public float killReward = 1.0f;
    public float existenceReward = -0.001f;
    public float missReward = -0.001f;
    public float fallReward = -1f;
    int counter = 0;
    Vector2 direction;

    private void Update()
    {
        distance = Mathf.Infinity;
        foreach (Transform child in transform.parent.transform)
            if (child.gameObject.tag == "Player" && !ReferenceEquals(gameObject, child.gameObject))
            {
                curDistance = (child.transform.localPosition - transform.localPosition).sqrMagnitude;
                if (curDistance < distance)
                {
                    distance = curDistance;
                    direction = (child.transform.localPosition - transform.localPosition).normalized;
                }
            }
        // TO DO: Popracowaæ nad parametrami funkcji
        AddReward(Mathf.Clamp(distanceReward * (-Mathf.Pow(0.01f * distance, 2f) - 10000f / Mathf.Pow(distance, 2f) + 5f), -distanceReward, distanceReward));
        counter = (counter + 1) % 100000;
        if (trainer != null && counter >= 10000)
        {
            counter = 0;
            trainer.GetComponent<Trainer>().NewLocation(gameObject);
            EndEpisode();
        }
    }

    public override void Initialize()
    {
        controller = GetComponent<Controller>();
        base.Initialize();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        SetParameters(sensor);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        controller.jump = vectorAction[0] > 0.5;
        controller.arrows.x = Mathf.Clamp(vectorAction[1], -1, 1);
        controller.shoot = vectorAction[2] > 0.5;
        controller.aimDirection = direction;
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetButton("Jump") ? 1 : 0;
        actionsOut[1] = Input.GetAxis("Horizontal");
        actionsOut[2] = Input.GetButton("Fire1") ? 1 : 0;
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public void SetParameters(VectorSensor sensor)
    {
        //sensor.AddObservation(transform.localPosition.x);
        //sensor.AddObservation(transform.localPosition.y);
        //sensor.AddObservation(platformerMotor.touch.ground);
        //sensor.AddObservation(platformerMotor.touch.ceiling);
        //sensor.AddObservation(platformerMotor.touch.left);
        //sensor.AddObservation(platformerMotor.touch.right);
        //sensor.AddObservation(killable.CurrentHP);
        //sensor.AddObservation(shoot.weapon.rateOfFire);
        //sensor.AddObservation(shoot.weapon.damagePerShot);
        //sensor.AddObservation(shoot.weapon.isAutomatic);
    }

    public void Dying()
    {
        AddReward(deathReward);
        EndEpisode();
        if (trainer != null) trainer.GetComponent<Trainer>().Respawn();
    }

    public void setTrainer(GameObject trainerObject)
    {
       trainer = trainerObject;
    }

    public void Killing()
    {
        AddReward(killReward);
        EndEpisode();
    }

    public void DealingDamage()
    {
        AddReward(damageReward);
    }

    public void MissingAttack()
    {
        AddReward(missReward);
    }

    public void Falling()
    {
        AddReward(fallReward);
        EndEpisode();
    }
}