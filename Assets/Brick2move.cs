using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;
using Ubiq.Messaging;
using static EnvironmentManager;

public class Brick2move : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    public Hand grasped;
    NetworkId INetworkObject.Id => NetworkId.Unique(); //new NetworkId(1001);// 
    private NetworkContext context;
    private int move_time;
    private Vector3 last_pos = Vector3Int.zero;
    private bool last_grasp = false;
    private Vector3 VarGrid = Vector3Int.zero;
    private Vector3Int PosStartPut = Vector3Int.zero;
    private Vector3Int PosEndPut = Vector3Int.zero;
    private Vector3Int MinBound = Vector3Int.zero;
    private Vector3Int MaxBound = new Vector3Int(20, 20, 20);
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
        VarGrid.x = 0.5f * transform.localScale.x % 2;
        VarGrid.y = 0.5f * transform.localScale.y % 2;
        VarGrid.z = 0.5f * transform.localScale.z % 2;
        //Debug.Log(VarGrid);
        context = NetworkScene.Register(this);
        Debug.Log("Register one brick");
        move_time = 0;
    }
    struct Message
    {
        public Vector3 position;
    }
    private bool HasSpaceInMap(Vector3Int start, Vector3Int end)
    {
        for (int i = start.x; i < end.x; i++)
        {
            for (int j = start.y; i < end.y; i++)
            {
                for (int k = start.z; k < end.z; i++)
                {
                    if (SpaceMap[i, j, k] != 0)
                    {
                        Debug.Log("Place being taken, Can't put");
                        return false;
                    }
                }
            }
        }
        return true;
    }
    private void FillMap(Vector3Int start, Vector3Int end)
    {
        for (int i = start.x; i < end.x; i++)
        {
            for (int j = start.y; i < end.y; i++)
            {
                for (int k = start.z; k < end.z; i++)
                {
                    SpaceMap[i, j, k] = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void DestroyScriptInstance()
    {
        // Removes this script instance from the game object
        Destroy(this);
    }
    public void DeleteBrick()
    {
        Debug.Log("Delete Brick at" + transform.localPosition);
        if (grasped)
        {
            DestroyScriptInstance();
        }
    }
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
            last_pos = message.position; //Tracking the bricks moved by the players.
            playerLocation = message.position;
            if (last_grasp == false)
            {
                PosStartPut = Vector3Int.RoundToInt(new Vector3(last_pos.x - (transform.localScale.x / 2) + 10,
                    last_pos.y - (transform.localScale.y / 2), last_pos.z - (transform.localScale.z / 2) + 10));
                PosEndPut = Vector3Int.RoundToInt(new Vector3(last_pos.x + (transform.localScale.x / 2) + 10,
                    last_pos.y + (transform.localScale.y / 2), last_pos.z + (transform.localScale.z / 2) + 10));
                if (Vector3.Min(PosStartPut, MinBound) != MinBound || Vector3.Max(PosEndPut, MaxBound) != MaxBound)
                {
                    Debug.Log("Min(PosStartPut, MinBound)" + Vector3.Min(PosStartPut, MinBound));
                    Debug.Log("Max(PosEndPut, MaxBound)" + Vector3.Max(PosEndPut, MaxBound));
                    Debug.Log("Over the Playing Boundary!");
                }
                else
                {
                    if (HasSpaceInMap(PosStartPut, PosEndPut))
                    {
                        FillMap(PosStartPut, PosEndPut);
                        Instantiate(transform, new Vector3(last_pos.x, last_pos.y, last_pos.z), Quaternion.identity, transform.parent);
                    }
                    else
                    {
                        Debug.Log("Place taken");
                    }
                }
            }
            last_grasp = true;
            
        }
        else if (last_grasp)
        {
            Debug.Log("Put Down");
            last_grasp = false;
            
            if (last_pos!=null)
            {
                //Debug.Log(last_pos);
                //Instantiate(transform, new Vector3(last_pos.x, last_pos.y, last_pos.z), Quaternion.identity);
                PosStartPut = Vector3Int.RoundToInt(new Vector3(last_pos.x - (transform.localScale.x / 2) + 10, 
                    last_pos.y - (transform.localScale.y / 2), last_pos.z - (transform.localScale.z / 2) + 10));
                PosEndPut = Vector3Int.RoundToInt(new Vector3(last_pos.x + (transform.localScale.x / 2) + 10, 
                    last_pos.y + (transform.localScale.y / 2), last_pos.z + (transform.localScale.z / 2) + 10));
                if(Vector3.Min(PosStartPut, MinBound)!= MinBound || Vector3.Max(PosEndPut, MaxBound) != MaxBound)
                {
                    Debug.Log("Min(PosStartPut, MinBound)" + Vector3.Min(PosStartPut, MinBound));
                    Debug.Log("Max(PosEndPut, MaxBound)" + Vector3.Max(PosEndPut, MaxBound));
                    Debug.Log("Over the Playing Boundary!");
                }else
                {
                    if (HasSpaceInMap(PosStartPut, PosEndPut))
                    {
                        FillMap(PosStartPut, PosEndPut);
                        Instantiate(transform, new Vector3(last_pos.x, last_pos.y, last_pos.z), Quaternion.identity, transform.parent);
                    }
                    else
                    {
                        Debug.Log("Place taken");
                    }
                    //Do nothing
                }
            } 
        }
        if (Time.time > 2.0 && move_time == 0)
        {
            //Debug.Log(Time.time);
            move_time = 1;
            //transform.localPosition = transform.localPosition + Vector3Int.right;
            //Instantiate(transform, transform.localPosition + Vector3Int.right, Quaternion.identity, transform.parent);

        }
        else if(Time.time >3.0 && move_time == 1)
        {
            
            
        }
    }
}