using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteMissionController : MonoBehaviour
{
    public int assemblyCounter = 0, partsRequired = 2;
    public bool shrinkOnAssembly = true;

    private TMPro.TextMeshProUGUI assemblyCounterText;
    // Start is called before the first frame update
    void Start()
    {
        //get elemnet with the name DebugDialog. It has a child with the name Dialog_Text which has a child with the name Text. This is the TextMeshProTextUI object
        assemblyCounterText = GameObject.Find("DebugDialog").transform.Find("Dialog_Text").Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(partsRequired == assemblyCounter)
        {
            Debug.Log("Mission Complete!");
            partsRequired = 0;

            if(shrinkOnAssembly)
            {
                //get the local scale of the object
                Vector3 localScale = transform.localScale;
                //set the x, y, and z scale to 0.1 (10% of the original size)
                Vector3 newScale = new Vector3(localScale.x/10, localScale.y/10, localScale.z/10);
                transform.localScale = newScale;
            }

            MissionManager missionManager = FindObjectOfType<MissionManager>();
            missionManager.BringRocketToLaunch();
        }
    }

    public void UpdateAssemblyCounter()
    {
        assemblyCounter++;
        if(assemblyCounterText != null)
            assemblyCounterText.text = "Assembly Counter: " + assemblyCounter;
    }
}
