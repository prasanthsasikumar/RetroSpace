using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public GameObject[] missionPrefabs;
    public Transform spawnPoint;
    public GameObject completedMissionPrefab, heatSinkPrefab;
    public float liftHeight = 1;
    public bool spawnAtTable = false;
    public GameObject rocketSpawner;
    private FindSpawnPositionsModified findSpawnPositionsModfied;
    // Start is called before the first frame update
    void Start()
    {
        findSpawnPositionsModfied = FindObjectOfType<FindSpawnPositionsModified>();
        //SpawnAllPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAllPrefabs()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        if(spawnAtTable)
            spawnPoint = findSpawnPositionsModfied.GetNearestTable();

        Vector3 UpdatedSpawnPointPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y + liftHeight, spawnPoint.position.z);
        
        foreach (GameObject missionPrefab in missionPrefabs)
        {
            //Adjust updatedSpawn position with a a very small x or z value to avoid overlapping objects. Make it random. Ideally 02.-.4
            //get size of missionPrefab
            float breadth = missionPrefab.GetComponent<Renderer>().bounds.size.x;
            Vector3 adjustedPosition = new Vector3(UpdatedSpawnPointPosition.x + breadth, UpdatedSpawnPointPosition.y, UpdatedSpawnPointPosition.z);
            GameObject satellitePart = Instantiate(missionPrefab, adjustedPosition, spawnPoint.rotation);
            satellitePart.transform.SetParent(spawnPoint);

            // Wait for 0.5 seconds (adjust the delay time as needed)
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void DropHeatSink(){
        Instantiate(heatSinkPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void BringRocketToLaunch()
    {
        //Delete every object that has a tag "Sat_part"
        GameObject[] satelliteParts = GameObject.FindGameObjectsWithTag("Sat_part");
        foreach (GameObject satellitePart in satelliteParts)
        {
            Destroy(satellitePart);
        }
        //Instantiate the completed mission prefab at the spawn point
        Instantiate(completedMissionPrefab, spawnPoint.position, spawnPoint.rotation);
        rocketSpawner.SetActive(true);
    }
}
