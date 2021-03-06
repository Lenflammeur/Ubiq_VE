using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;
namespace Ubiq.Samples
{
    public class Cube : MonoBehaviour, INetworkObject, INetworkComponent, IGraspable, ISpawnable
    {
        private NetworkContext context;
        //public PrefabCatalogue StCatalogue;
        private Hand grasped;
        private Vector3 VarGrid = Vector3Int.zero;
        private int count1 = 0;
        //public GameObject MyPrefab;
        public bool owner = true;

        public struct Message
        {
            public TransformMessage transform;
            public Color colour;

            public Message(Transform transform, Color colour)
            {
                this.transform = new TransformMessage(transform);
                this.colour = colour;

            }
        }
        public NetworkId Id { get; set; } = new NetworkId(); // the network Id will be set by the spawner,
                                                                  // which will always be the one
        // to instantiate the cubes
        private void Start()
        {
            count1 = 0;
            //Debug.Log(StCatalogue);
            //MyPrefab = StCatalogue.prefabs[0];
            context = NetworkScene.Register(this);
            VarGrid.x = 0.5f * transform.localScale.x % 2;
            VarGrid.y = 0.5f * transform.localScale.y % 2;
            VarGrid.z = 0.5f * transform.localScale.z % 2;

        }

        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            var msg = message.FromJson<Message>();

            transform.localPosition = msg.transform.position; // The Message constructor will take the *local* properties of the passed transform.
            transform.localRotation = msg.transform.rotation;
            GetComponentInChildren<Renderer>().material.SetColor("_Color", msg.colour);
        }


        private void Update()
        {
            if (owner)
            {
                context.SendJson(new Message(transform, GetComponentInChildren<Renderer>().material.color));
            }
            if (grasped)
            {
                //Debug.Log("grasped");
                //Vector3 roundTrans = Vector3Int.RoundToInt(grasped.transform.position - VarGrid) + VarGrid;
                //Debug.Log(roundTrans);
                transform.position = grasped.transform.position;
                transform.rotation = grasped.transform.rotation;
                context.SendJson(new Message(transform, GetComponentInChildren<Renderer>().material.color));

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
            owner = local;
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