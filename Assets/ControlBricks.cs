using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Brick2move;

public class ControlBricks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DeleteBrick()
    {
        if (transform.childCount > 0)
        {
            Debug.Log("Cleaning map...");
            while (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                
            }
        }
    }
}
