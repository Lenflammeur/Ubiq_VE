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
        private Renderer graspedRenderer;
        

        private void Change_color()
        {
            Debug.Log("haha");
            graspedRenderer.sharedMaterial.SetColor("_Color", color);
        }

        // Start is called before the first frame update
        void Start()
        {
            graspedRenderer = grasped.GetComponentInChildren<Renderer>();
            gameObject.GetComponent<Button>().onClick.AddListener(Change_color);
        }

        // Update is called once per frame
        void Update()
        {
  
        }
    }
}