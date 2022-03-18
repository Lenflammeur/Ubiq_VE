using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ubiq.XR;

namespace Ubiq.Samples
{
    public class Change_scale : MonoBehaviour
    {
        [SerializeField]
        private GameObject grasped;
        [SerializeField]
        private float scale;
        private Transform graspedTransform;


        private void change_scale()
        {
            Debug.Log(graspedTransform.GetChild(0).localScale);
            graspedTransform.localScale.Set(scale, scale, scale);
        }

        // Start is called before the first frame update
        void Start()
        {
            graspedTransform = grasped.GetComponent<RectTransform>();
            gameObject.GetComponent<Button>().onClick.AddListener(change_scale);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}