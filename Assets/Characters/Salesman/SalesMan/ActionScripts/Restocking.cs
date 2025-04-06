using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;
using static UnityEngine.GraphicsBuffer;

public class Restocking : AntAIState
{
    public GameObject StreetVendor;
    public NeighbourTracker p_NeighbourTracker;
    public MoveFoward p_MoveFoward;
    public float restockRange = 10f;

    //directional Scripts
    public Avoid p_Avoiding;
    public TurnTowards p_TurnTowards;
    public NavigationScript p_NavigatingScript;
    public PathFollow p_PathFollow;
    public StreetVendor_Model vendor;


    public override void Create(GameObject aGameObject)
    {
       base.Create(aGameObject);
       StreetVendor = aGameObject;
       p_MoveFoward = StreetVendor.GetComponent<MoveFoward>();
       p_NeighbourTracker = StreetVendor.GetComponent<NeighbourTracker>();

       //directional Scripts
       p_Avoiding = StreetVendor.GetComponent<Avoid>();
       p_TurnTowards = StreetVendor.GetComponent<TurnTowards>();
       p_NavigatingScript = StreetVendor.GetComponent<NavigationScript>();
       p_PathFollow= StreetVendor.GetComponent<PathFollow>();
       vendor = StreetVendor.GetComponent<StreetVendor_Model>();

    }
    public override void Enter()
    {
       base.Enter();
       Debug.Log("AI is now Restocking");
       p_MoveFoward.enabled = true;
       p_Avoiding.enabled = true;
       p_TurnTowards.enabled = true;
       p_NavigatingScript.enabled = true;
       p_PathFollow.enabled = true;
       GetComponentInParent<Renderer>().material.color = Color.grey;

       p_NavigatingScript.target = vendor.restockLocation;




    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);


        if (Vector3.Distance(StreetVendor.transform.position, vendor.restockLocation.position) <= restockRange)
        {
            vendor.hasStock = true;
            Debug.Log("AI has restocked");
            Finish();
        }
    }

    public override void Exit()
    {
        
        base.Exit();
        p_MoveFoward.enabled = false;
        p_Avoiding.enabled = false;
        p_TurnTowards.enabled = false;
        p_NavigatingScript.enabled = false;
        p_PathFollow.enabled = false;
    }
}
