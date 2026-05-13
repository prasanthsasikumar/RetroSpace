using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPositionAndRotationOncePlacedOnTable : MonoBehaviour
{
    string tableColliderName = "TABLE_EffectMesh";
    public float timeBeforeLocking = 2;
    private bool isPlacedOnTable = false;
    private float timeOfStart = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeOfStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //return if the time has not passed
        if(Time.time - timeOfStart < timeBeforeLocking)
        {
            return;
        }
        if(!isPlacedOnTable)
        {
            CheckIfPlacedOnTable();
        }
    }

    private void CheckIfPlacedOnTable()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.name.Contains(tableColliderName))
            {
                isPlacedOnTable = true;
                LockPositionAndRotation();
                break;
            }
        }
    }

    private void LockPositionAndRotation()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Position and Rotation Locked");
        }
    }
}
