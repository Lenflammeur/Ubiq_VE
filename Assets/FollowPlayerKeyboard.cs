using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnvironmentManager;
public class FollowPlayerKeyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = playerLocation + new Vector3(0f, 0f, 2f);
    }
}
