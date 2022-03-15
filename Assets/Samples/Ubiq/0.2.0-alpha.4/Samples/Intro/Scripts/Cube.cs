using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

namespace Ubiq.Samples
{
    public class Cube : MonoBehaviour, INetworkObject, INetworkComponent, IGraspable, ISpawnable
    {
        private NetworkContext context;

        private Hand grasped;

        int n = 0;

        public NetworkId Id { get; set; } // the network Id will be set by the spawner, which will always be the one to instantiate the Firework

        

        private void Start()
        {
            //context = NetworkScene.Register(this);
            
        }

        /*public void Attach(Hand hand)
        {
            Debug.Log("Attach_Cube");
            attached = hand;
        }*/

        


        private void Update()
        {
            if (grasped)
            {
                transform.position = grasped.transform.position;
                transform.rotation = grasped.transform.rotation;
                //GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.blue);
            }

            if (Input.GetKeyDown(KeyCode.K) && grasped)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
            }

            if (Input.GetKeyDown(KeyCode.L) && grasped)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.blue);
            }

            if (Input.GetKeyDown(KeyCode.M) && grasped)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.yellow);
            }
        }

        public void Change_red()
        {
            if ( n == 0)
            {
                Debug.Log(n);
               
                n++;
            }
            
            
        }

        public void OnSpawned(bool local)
        {
        }

        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            throw new System.NotImplementedException();
        }

        public void Grasp(Hand controller)
        {
            grasped = controller;
        }

        public void Release(Hand controller)
        {
            Debug.Log("released");
            grasped = null;
        }
    }

   
}