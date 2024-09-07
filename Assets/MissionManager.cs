using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public GameObject[] missionPrefabs;
    public Transform spawnPoint;
    public float liftHeight = 1;
    private FindSpawnPositionsModified findSpawnPositionsModfied;
    // Start is called before the first frame update
    void Start()
    {
        findSpawnPositionsModfied = FindObjectOfType<FindSpawnPositionsModified>();
        SpawnAllPrefabs();
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
        //spawnPoint = findSpawnPositionsModfied.GetNearestTable();
        Vector3 UpdatedSpawnPointPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y + liftHeight, spawnPoint.position.z);
        
        foreach (GameObject missionPrefab in missionPrefabs)
        {
            GameObject satellitePart = Instantiate(missionPrefab, UpdatedSpawnPointPosition, spawnPoint.rotation);
            satellitePart.transform.SetParent(spawnPoint);

            // Wait for 0.5 seconds (adjust the delay time as needed)
            yield return new WaitForSeconds(0.4f);
        }
    }
}
