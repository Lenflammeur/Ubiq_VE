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

            if (Input.GetKeyDown(KeyCode.R) && grasped)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
            }

            if (Input.GetKeyDown(KeyCode.B) && grasped)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.blue);
            }

            if (Input.GetKeyDown(KeyCode.G) && grasped)
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && grasped)
            {
                transform.localScale = transform.localScale * .5f;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && grasped)
            {
                transform.localScale = transform.localScale / 0.5f;
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