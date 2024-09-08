using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseTheLidAndLiftOff : MonoBehaviour
{
    public GameObject lid,fireSmoke;
    public Transform lidAnchor;

    public float timeToCloseLid = .5f;

    public UnityEvent onLidClosed, onRocketLaunchSuccess;
    // Start is called before the first frame update
    void Start()
    {
        //CloseLidAndLiftOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseLidAndLiftOff()
    {
        StartCoroutine(CloseLid());
    }

    public IEnumerator CloseLid()
    {
        float elapsedTime = 0;
        Vector3 startLidPosition = lid.transform.position;
        //end position is 0.1 below its current position
        Vector3 endLidPosition = new Vector3(lid.transform.position.x, lid.transform.position.y - 1.6f, lid.transform.position.z);
        while (elapsedTime < timeToCloseLid)
        {
            lid.transform.position = Vector3.Lerp(startLidPosition, endLidPosition, (elapsedTime / timeToCloseLid));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        lid.transform.position = endLidPosition;
        Debug.Log("Lid closed");
        onLidClosed.Invoke();
    }

    public void LiftOff()
    {
        Debug.Log("Lift off!");
        //remove rigidbody from all the children of the rocket
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            Destroy(rb);
        }
        fireSmoke.SetActive(true);
        // Start the coroutine to lift off over 10 seconds
        StartCoroutine(LiftOffCoroutine());
    }

    private IEnumerator LiftOffCoroutine()
    {
        float duration = 10f;
        float time = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 25); // Add 15 degrees of tilt along the Z-axis, adjust as needed

        Debug.Log("Start Position: " + startPosition + " End Position: " + endPosition);

        while (time < duration)
        {
            // Interpolate position
            transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);

            // Interpolate rotation (tilt)
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);

            time += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the final position and rotation are exactly at the end
        transform.position = endPosition;
        transform.rotation = endRotation;

        // Optionally, destroy the object after reaching the top
        // Destroy(gameObject);

        onRocketLaunchSuccess.Invoke();
    }

    
}
