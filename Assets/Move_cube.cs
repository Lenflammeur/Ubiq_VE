using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;
using Ubiq.Messaging;

public class Move_cube : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject

{
    private Hand grasped;
    NetworkId INetworkObject.Id => new NetworkId(1001);
    private NetworkContext context;
    private int move_time;
    private Vector3 dis_unit_2right = new Vector3(1,0,0);
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
        move_time = 0;
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
            Debug.Log("Grasped");
            transform.localPosition = grasped.transform.position;
            Message message;
            message.position = transform.localPosition;
            context.SendJson(message);
            Debug.Log("Grasped");
        }
        if (Time.time > 2.0 && move_time == 0)
        {
            Debug.Log(Time.time);
            move_time = 1;
            transform.localPosition = transform.localPosition + dis_unit_2right;
        }else if(Time.time >3.0 && move_time == 1)
        {
            
            
        }
    }
}