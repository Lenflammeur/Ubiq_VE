using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ubiq.XR;

namespace Ubiq.Samples
{
    public class change_color : MonoBehaviour
    {
        [SerializeField]
        private GameObject grasped;
        [SerializeField]
        private Color color;
        private Material graspedRenderer;
        
       

        private void Change_color()
        {
            GameObject selectedObject = Instantiate(grasped);
            graspedRenderer = selectedObject.GetComponentInChildren<Renderer>().material;
            graspedRenderer.SetColor("_Color", color);

        }

        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Change_color);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}