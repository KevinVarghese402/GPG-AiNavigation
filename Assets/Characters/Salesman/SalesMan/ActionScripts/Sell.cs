using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

public class Sell : AntAIState
{
    public MoveFoward p_MoveForwad;
    public GameObject StreetVendor;
    public StreetVendor_Model p_StreetVendorModel;
    public NavigationScript p_NavigatingScript;
    public NeighbourTracker p_NeighbourTracker;
    public Customer_TestModel p_customerModel;
        
    public override void Create(GameObject aGameObject)
    {
        base.Create(aGameObject);
        StreetVendor = aGameObject;
        p_MoveForwad = StreetVendor.GetComponent<MoveFoward>();
        p_NeighbourTracker = StreetVendor.GetComponent<NeighbourTracker>();
        p_StreetVendorModel = StreetVendor.GetComponent<StreetVendor_Model>();

    }
    public override void Enter()
    {
        base.Enter();
        p_MoveForwad.enabled = true;
        p_NeighbourTracker.enabled = true;

        p_StreetVendorModel.hasStock = false;
   

    }

    public override void Execute(float aDeltaTime, float aTimeScale) //update while in the state // Then needing a Finish
    {
        base.Execute(aDeltaTime, aTimeScale);


    }

    public override void Exit()
    {
        p_MoveForwad.enabled = false;
        p_NeighbourTracker.enabled = false;
        p_StreetVendorModel.hasStock = false;
        base.Exit();
    }
}
