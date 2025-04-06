using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

public enum StreetVendor
{
    NearFIre = 0,
    NearCustomers = 1,
    HasMoney = 2,
    HasStock = 3
}


public class StreetVendor_Sensor : MonoBehaviour, ISense
{
    
    public bool NearFire;
    public bool NearCustomer;
    public float maxDistanceNearCustomer; 

    //public Vision vision;
    public NeighbourTracker neighbourTracker;
    public StreetVendor_Model streetVendor;

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        NearFire = false;
        NearCustomer = false;

        foreach (Transform neighbour in neighbourTracker.neighbours)
        {
            if (neighbour.GetComponent<Customer_TestModel>())
            {
                if (Vector3.Distance(neighbour.transform.position, transform.position) <= maxDistanceNearCustomer )
                {
                    Debug.Log("Customer detected!");
                    NearCustomer = true;
                }
              
            }
            if(neighbour.GetComponent<FireTestModel>())
            {
                NearFire = true;
            }
        }

        aWorldState.Set(StreetVendor.NearFIre, NearFire);       
        aWorldState.Set(StreetVendor.NearCustomers, NearCustomer); 
        aWorldState.Set(StreetVendor.HasMoney, streetVendor.hasMoney);
        aWorldState.Set(StreetVendor.HasStock, streetVendor.hasStock);
    }



}
