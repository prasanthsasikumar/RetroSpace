using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUpWhenSpawn : MonoBehaviour
{
    public bool faceCamera = false;
    public float liftHeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        //When the prefab is loaded, pull it up 1 meter so it doesn't spawn inside the floor
        transform.position = new Vector3(transform.position.x, transform.position.y + liftHeight, transform.position.z);
        if (faceCamera)
        {
            transform.LookAt(Camera.main.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
