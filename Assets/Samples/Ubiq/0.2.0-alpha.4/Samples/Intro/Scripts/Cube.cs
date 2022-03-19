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
        private Vector3 last_pos = Vector3Int.zero;
        private bool last_grasp = false;
        private Vector3 VarGrid = Vector3Int.zero;
        private int count1 = 0;
        //public GameObject MyPrefab;
        public NetworkId Id { get; set; } // the network Id will be set by the spawner, which will always be the one to instantiate the Firework



        private void Start()
        {
            count1 = 0;
            //Debug.Log(StCatalogue);
            //MyPrefab = StCatalogue.prefabs[0];
            //context = NetworkScene.Register(this);
            VarGrid.x = 0.5f * transform.localScale.x % 2;
            VarGrid.y = 0.5f * transform.localScale.y % 2;
            VarGrid.z = 0.5f * transform.localScale.z % 2;

        }
        /*
        public void SetPrefab(GameObject Prefab)
        {
            prefab = Prefab;
            Debug.Log(Prefab.name);
            Prefab.name = "Cube";
        }*/
        /*public void Attach(Hand hand)
        {
            Debug.Log("Attach_Cube");
            attached = hand;
        }*/


        private void Update()
        {
            if (grasped)
            {
                //Debug.Log("grasped");
                //Vector3 roundTrans = Vector3Int.RoundToInt(grasped.transform.position - VarGrid) + VarGrid;
                //Debug.Log(roundTrans);
                transform.position = grasped.transform.position;
                /*if(count1 == 0)
                {
                    var cube = NetworkSpawner.SpawnPersistent(this, MyPrefab).GetComponents<MonoBehaviour>().Where(mb => mb is IGraspable).FirstOrDefault() as IGraspable;
                }*/

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