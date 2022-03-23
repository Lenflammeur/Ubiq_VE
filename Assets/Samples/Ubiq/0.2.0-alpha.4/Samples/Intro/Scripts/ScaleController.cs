using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    // Start is called before the first frame update
    public static float cubescale = 1.0f;
    public static Vector3 lastposition = Vector3.zero;
    void Start()
    {
        
    }
    public void small()
    {
        cubescale = 0.25f;
    }
    public void middle()
    {
        cubescale = 0.5f;
    }
    public void Big()
    {
        cubescale = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
