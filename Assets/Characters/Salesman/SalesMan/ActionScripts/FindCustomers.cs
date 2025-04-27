using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

namespace KV
{
    public class FindCustomers : AntAIState
    {
        public Customer_TestModel p_customerModel;
        public GameObject StreetVendor;

        //directional Scripts 
        public NeighbourTracker p_NeighbourTracker;

        public TurnTowards p_TurnTowards;
        public PathFollow p_PathFollow;
        public NavigationScript p_NavigatingScript;


        public MoveFoward p_MoveFoward;
        public Avoid p_Avoiding;
        public Cohesion p_Cohesion;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            StreetVendor = aGameObject;
            p_MoveFoward = StreetVendor.GetComponent<MoveFoward>();

            //directional
            p_MoveFoward = StreetVendor.GetComponent<MoveFoward>();
            p_Avoiding = StreetVendor.GetComponent<Avoid>();
            p_Cohesion = StreetVendor.GetComponent<Cohesion>();
            p_NeighbourTracker = StreetVendor.GetComponent<NeighbourTracker>();
            p_TurnTowards = StreetVendor.GetComponent<TurnTowards>();
            p_PathFollow = StreetVendor.GetComponent<PathFollow>();
            p_NavigatingScript = StreetVendor.GetComponent<NavigationScript>();

        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("AI is now FindingTheFire");
            p_MoveFoward.enabled = true;
            p_Avoiding.enabled = true;
            p_Cohesion.enabled = true;
            p_NeighbourTracker.enabled = true;
            p_TurnTowards.enabled = true;
            p_PathFollow.enabled = true;
            p_NavigatingScript.enabled = true;


            foreach (Transform neighbour in p_NeighbourTracker.neighbours)
            {
                if (neighbour.GetComponent<Customer_TestModel>())
                {
                    p_customerModel = neighbour.GetComponent<Customer_TestModel>();
                    p_NavigatingScript.target = p_customerModel.transform;
                    break;
                }
            }

        }

        public override void Execute(float aDeltaTime, float aTimeScale) //update while in the state // Then needing a Finish
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (Vector3.Distance(p_customerModel.transform.position, transform.position) <= StreetVendor.GetComponent<StreetVendor_Sensor>().maxDistanceNearCustomer)
            {
                Finish();
            }

        }

        public override void Exit()
        {
            p_MoveFoward.enabled = false;
            p_Avoiding.enabled = false;
            p_Cohesion.enabled = false;
            p_TurnTowards.enabled = false;
            p_PathFollow.enabled = false;
            p_NavigatingScript.enabled = false;

            base.Exit();
        }
    }
}
