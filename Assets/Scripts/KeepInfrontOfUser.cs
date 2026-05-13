using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInfrontOfUser : MonoBehaviour
{
    public bool moveOnlyonStart = true;
    // Start is called before the first frame update
    void Start()
    {
        if (moveOnlyonStart)
        {
            MoveObject();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveOnlyonStart)
        {
            MoveObject();
        }
    }

    public void MoveObject()
    {
        //Make the object face the camera
        transform.LookAt(Camera.main.transform);
        //Make the object stay in front of the camera
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }
}
