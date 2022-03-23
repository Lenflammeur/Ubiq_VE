using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnvironmentManager;
public class UpdateLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = this.transform.localPosition;
        
    }
}
