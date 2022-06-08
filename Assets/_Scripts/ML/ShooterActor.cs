using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class ShooterActor : Agent
{
    Controller controller;
    AutoAim autoAim;

    public override void Initialize()
    {
        controller = GetComponent<Controller>();
        base.Initialize();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // grounded
        // on the edge
        // before step

        // health:
        // hp
        // direction of damage 

        // auto aim:
        // type of closest item: box, weapon, explosive 
        // hp of closest character
        // direction to closest character
        sensor.AddObservation(0);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        controller.jump = vectorAction[0] > 0.5;
        controller.arrows.x = vectorAction[1];
        controller.arrows.y = vectorAction[2];
        controller.shoot = vectorAction[3] > 0.5;
        // aim at item
        // aim at character
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetButton("Jump") ? 1 : 0;
        actionsOut[1] = Input.GetAxis("Horizontal") ;
        actionsOut[2] = Input.GetAxis("Vertical");
        actionsOut[3] = Input.GetButton("Fire1") ? 1 : 0;
    }
    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }
}