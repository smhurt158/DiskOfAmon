using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    Vector3 move = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0, .05f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y > 2.5)
        {
            move = new Vector3(0, -.0005f, 0);
        }
        else if(gameObject.transform.position.y < -3)
        {
            move = new Vector3(0, .0005f, 0);
        }
        gameObject.transform.position += move;
    }
}
