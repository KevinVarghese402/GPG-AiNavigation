using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

namespace KV
{
    public class Sell : AntAIState
    {
        public MoveFoward p_MoveForwad;
        public GameObject StreetVendor;
        public StreetVendor_Model p_StreetVendorModel;
        public NavigationScript p_NavigatingScript;
        public NeighbourTracker p_NeighbourTracker;
        public Customer_TestModel p_customerModel;

        public float timeToSell = 1f;
        private float sellTimer;

        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            StreetVendor = aGameObject;
            p_NeighbourTracker = StreetVendor.GetComponent<NeighbourTracker>();
            p_StreetVendorModel = StreetVendor.GetComponent<StreetVendor_Model>();


        }
        public override void Enter()
        {
            base.Enter();
            sellTimer = Time.time;

        }

        public override void Execute(float aDeltaTime, float aTimeScale) //update while in the state // Then needing a Finish
        {
            base.Execute(aDeltaTime, aTimeScale);
            if (Time.time - sellTimer >= timeToSell)
            {
                p_StreetVendorModel.hasStock = false;
                //p_StreetVendorModel.hasMoney = true;

                Finish();
            }

        }

    }
}
