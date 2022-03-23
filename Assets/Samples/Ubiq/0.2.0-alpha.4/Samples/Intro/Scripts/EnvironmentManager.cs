using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static Vector3 playerLocation = Vector3Int.zero;
    public static Vector3 playerRotation = Vector3Int.zero;
    public static Vector3 lastBrick = Vector3Int.zero;
    private Vector3 lastLocation = Vector3Int.zero;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("EnvironmentManager Awaken");
    }

    // Update is called once per frame
    void Update()
    {

        if(playerLocation!= lastLocation)
        {
            //Debug.Log("Player are moving with brick!");
            //Debug.Log(playerLocation);
            lastLocation = playerLocation;
        }
    }
}
