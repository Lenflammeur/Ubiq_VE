using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static int[,,] SpaceMap = new int[20,20,20];
    public static Vector3 playerLocation = Vector3Int.zero;
    public static Vector3 playerRotation = Vector3Int.zero;
    public static Vector3 lastBrick = Vector3Int.zero;
    private Vector3 lastLocation = Vector3Int.zero;
    // Start is called before the first frame update
    void Start()
    {
        int i, j, k;
        Debug.Log("EnvironmentManager Awaken");
        for(i=0; i<20; i++)
        {
            for(j = 0; j < 20; j++)
            {
                for (k = 0; k < 20; k++)
                {
                    SpaceMap[i, j, k] = 0;
                    //Debug.Log(SpaceMap[i,j, k]);
                }
            }
            
        }
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
