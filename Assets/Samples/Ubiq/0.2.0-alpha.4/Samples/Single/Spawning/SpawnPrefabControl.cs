using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Ubiq.XR;
using static EnvironmentManager;
using static ScaleController;

namespace Ubiq.Samples.Single.Spawning
{
    [RequireComponent(typeof(Button))]
    public class SpawnPrefabControl : MonoBehaviour
    {
        private GameObject prefab;
        public void SetPrefab(GameObject Prefab)
        {
            prefab = Prefab;
            GetComponentInChildren<Text>().text = prefab.name;
        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(Spawn);
        }

        void Spawn()
        {
            var cube = NetworkSpawner.Spawn(this, prefab);
            cube.transform.localScale = cube.transform.localScale * cubescale;
            //cube.transform.localRotation = GetComponent<PlayerController>().transform.rotation;
            if (playerLocation != lastposition)
            {
                cube.transform.position = playerLocation + new Vector3(0f, cube.transform.localScale.y, 1.2f + cube.transform.localScale.z);
                
            }else {
                Debug.Log(lastBrick);
                cube.transform.position = lastBrick + new Vector3(0f, cube.transform.localScale.y/2, 0f);
            }
            
            lastBrick = cube.transform.position;
            lastposition = playerLocation;

        }
    }
}