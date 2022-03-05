using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;
using Ubiq.Messaging;

public class Brick2move : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject

{
    private Hand grasped;
    NetworkId INetworkObject.Id => new NetworkId(1001);
    private NetworkContext context;
    private int move_time;
    private Vector3 halfGrid = new Vector3(0.5f,0.5f,0.5f);
    private Vector3 last_pos = Vector3Int.zero;
    private Vector3 VarGrid = Vector3Int.zero;
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
        float x, y, z;
        VarGrid.x = 0.5f * transform.localScale.x % 2;
        VarGrid.y = 0.5f * transform.localScale.y % 2;
        VarGrid.z = 0.5f * transform.localScale.z % 2;
        Debug.Log(VarGrid);
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
            Vector3 roundTrans = Vector3Int.RoundToInt(grasped.transform.position - VarGrid) + VarGrid;
            transform.localPosition = roundTrans; //relative to parent object
            Message message;
            message.position = transform.localPosition;
            context.SendJson(message);
            Debug.Log("Grasped");
            if(message.position != last_pos)
            {
                Debug.Log(message.position);
                Instantiate(transform, new Vector3(last_pos.x, last_pos.y + 1.0f, last_pos.z), Quaternion.identity);
            }
            last_pos = message.position;

        }
        if (Time.time > 2.0 && move_time == 0)
        {
            Debug.Log(Time.time);
            move_time = 1;
            transform.localPosition = transform.localPosition + Vector3Int.right;
            

        }
        else if(Time.time >3.0 && move_time == 1)
        {
            
            
        }
    }
}