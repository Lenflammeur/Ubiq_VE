using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Ubiq.Messaging;
using Ubiq.Rooms;
using Ubiq.Logging;
namespace Ubiq.Samples.Single.Spawning
{
    public class StaticCatalogue : MonoBehaviour
    {
        // Start is called before the first frame update
        public PrefabCatalogue StCatalogue;

        private List<SpawnPrefabControl> Controls;
        void Start()
        {
            Controls = GetComponentsInChildren<SpawnPrefabControl>(true).ToList();
            Debug.Log("Static Catalogue");
        }

        // Update is called once per frame
        void Update()
        {
            if(Controls.Count > 0)
            {
               
               UpdateCatalogue();
            }
            
        }
        public void UpdateCatalogue()
        {
            while (Controls.Count < StCatalogue.prefabs.Count)
            {
                var Control = GameObject.Instantiate(Controls.First().gameObject, transform);
                Controls.Add(Control.GetComponent<SpawnPrefabControl>());
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].gameObject.SetActive(i < StCatalogue.prefabs.Count);
            }

            for (int i = 0; i < StCatalogue.prefabs.Count; i++)
            {
                Debug.Log("Update");
                Debug.Log(StCatalogue.prefabs[0]);
                Controls[i].SetPrefab(StCatalogue.prefabs[0]);
            }
        }
    }
}
