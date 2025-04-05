using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

public class FindFire : AntAIState
{
    public GameObject StreetVendor;

    //directional Scripts 
    public NeighbourTracker p_NeighbourTracker;
    public MoveFoward p_MoveFoward;
    public Avoid p_Avoiding;
    public Cohesion p_Cohesion;

    //public StreetVendor_Sensor p_StreetVendor_Sensor; 

    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        StreetVendor = aGameObject;
        p_MoveFoward = StreetVendor.GetComponent<MoveFoward>();

        //directional
        p_MoveFoward = StreetVendor.GetComponent<MoveFoward>();
        p_Avoiding = StreetVendor.GetComponent<Avoid>();
        p_Cohesion = StreetVendor.GetComponent<Cohesion>();

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("AI is now FindingTheFire");
        p_MoveFoward.enabled = true;
        p_Avoiding.enabled = true;
        p_Cohesion.enabled = true;
        
    }

    public override void Execute(float aDeltaTime, float aTimeScale) //update while in the state // Then needing a Finish
    {
        base.Execute(aDeltaTime, aTimeScale);

        foreach (Transform neighbour in p_NeighbourTracker.neighbours)
        {
            if (neighbour.GetComponent<FireTestModel>())
            {
                Finish();
            }
        }
        
    }

    public override void Exit()
    {
        p_MoveFoward.enabled = false;
        p_Avoiding.enabled = false;
        p_Cohesion.enabled = false;
        //p_StreetVendor_Sensor.enabled = false;

        base.Exit();
    }
}
