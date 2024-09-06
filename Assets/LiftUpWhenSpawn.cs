using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftUpWhenSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //When the prefab is loaded, pull it up 1 meter so it doesn't spawn inside the floor
        transform.position = new Vector3(transform.position.x, transform.position.y + .7f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
