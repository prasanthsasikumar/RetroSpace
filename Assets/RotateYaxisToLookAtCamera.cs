using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYaxisToLookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Do not rotate the object on the x or z axis
        Vector3 lookAtPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
        transform.LookAt(lookAtPosition);
    }
}
