using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float left, right;
    public GameObject mario;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = mario.transform.position.x;
        var y = mario.transform.position.y;

        var cameraX = transform.position.x;
        var cameray = transform.position.y;

        if(x > left && y < right)
        {
            cameraX = x;
        }
        else
        {
            if (x < left) cameraX = left;
            if (x > right) cameraX = right;
        }

        if (y > 0)
        {
            cameray = y;
        }
        else
        {
            cameray = 0;
        }

        transform.position = new Vector3(cameraX, cameray, -10);
    }
}
