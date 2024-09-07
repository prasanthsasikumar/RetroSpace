using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachSatToRocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //wait to 2 seconds and then attach the satellite to the rocket
        StartCoroutine(WaitAndAttach());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitAndAttach()
    {
        //wait for 2 seconds
        yield return new WaitForSeconds(3);
        //find the gameobject with tag "Rocket"
        GameObject rocket = GameObject.FindGameObjectWithTag("rocket");
        //Lerp and attach the satellite to the rocket in 5 seconds
        float time = 0;
        float duration = 5;
        Vector3 startPosition = transform.position;
        //Rocket has a child called "bottom" which has a child "attachPoint" whch is the endPosition 
        Vector3 endPosition = rocket.transform.Find("Rocket_Bottom").Find("AttachPoint").position;
        Debug.Log("Start Position: " + startPosition + " End Position: " + endPosition);
        float originalScale = transform.localScale.x; // Assuming uniform scale
        float targetScale = originalScale * 0.1f; // 10% of the original size
        while (time < duration)
        {
            Debug.Log("Attaching satellite to rocket");
            
            // Move the satellite
            transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            
            // Randomly rotate the satellite
            transform.Rotate(Random.Range(0f, 360f) * Time.deltaTime, Random.Range(0f, 360f) * Time.deltaTime, Random.Range(0f, 360f) * Time.deltaTime);

            // Gradually shrink the satellite to 10% of its original size
            float scale = Mathf.Lerp(originalScale, targetScale, time / duration);
            transform.localScale = new Vector3(scale, scale, scale);

            time += Time.deltaTime;
            yield return null;
        }
        transform.SetParent(rocket.transform);
    }
}
