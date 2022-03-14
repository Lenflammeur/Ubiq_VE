using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class Ball : MonoBehaviour, IGraspable, INetworkComponent
{

    private Hand grasped;
    void IGraspable.Grasp(Hand controller)
    {
        grasped = controller;
    }

    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        throw new System.NotImplementedException();
    }

    void IGraspable.Release(Hand controller)
    {
        grasped = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grasped)
        {
            transform.localPosition = grasped.transform.position;
        }
    }
}
