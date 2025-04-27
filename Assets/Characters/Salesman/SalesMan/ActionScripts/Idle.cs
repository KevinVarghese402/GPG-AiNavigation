using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anthill.AI;

namespace KV
{
    public class Idle : AntAIState
    {
        public GameObject StreetVendor;
        public MoveFoward p_MoveFoward;
        public Avoid p_Avoiding;
        public override void Create(GameObject aGameObject)
        {
            base.Create(aGameObject);
            StreetVendor = aGameObject;
            p_MoveFoward = StreetVendor.GetComponent<MoveFoward>();
            p_Avoiding = StreetVendor.GetComponent<Avoid>();
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("AI is now Wandering");
            p_MoveFoward.enabled = true;
            p_Avoiding.enabled = true;
            GetComponentInParent<Renderer>().material.color = Color.grey;


        }

        public override void Exit()
        {
            base.Exit();
            p_MoveFoward.enabled = false;
            p_Avoiding.enabled = false;
        }
    }
}
