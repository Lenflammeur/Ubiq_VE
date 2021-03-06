using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;
using Ubiq.Messaging;

public class Ball : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject

{
    private Hand grasped;
    NetworkId INetworkObject.Id => new NetworkId(1001);
    private NetworkContext context;

    void IGraspable.Grasp(Hand controller)
    {
        grasped = controller;
    }

    void IGraspable.Release(Hand controller)
    {
        grasped = null;
    }
    void INetworkComponent.ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        transform.localPosition = msg.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
    }
    struct Message
    {
        public Vector3 position;
    }
    // Update is called once per frame
    void Update()
    {
        if (grasped)
        {
            transform.localPosition = grasped.transform.position;
            Message message;
            message.position = transform.localPosition;
            context.SendJson(message);
            Debug.Log("Grasped");
        }
    }
}
