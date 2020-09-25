using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn;

    [SerializeField] private float spawnInterval=1;
    private float timeDecreaseRate = 0.98f;
    private int spawnCountForDecrease = 3;

    private bool spawnBirds=false;
    public bool SpawnBirds
    {
        get => spawnBirds;
        set
        {
            spawnBirds = value;
            StartCoroutine(SpawnCoroutine());
        }
    }


    private IEnumerator SpawnCoroutine()
    {
        float time = 0;
        int spawnCount = 0;
        bool spawned=false;

        while (spawnBirds)
        {
            if (time >= spawnInterval)
            {
                /*
                foreach(GameObject obj in objectsToSpawn)
                {
                    if(!obj.activeInHierarchy)
                    {
                        obj.SetActive(true);
                        break;
                    }
                }
                */
                
                if(!spawned)
                {
                    int rand = (int)Random.Range(0, objectsToSpawn.Count);
                    if (!objectsToSpawn[rand].activeInHierarchy)
                    {
                        objectsToSpawn[rand].SetActive(true);
                        spawnCount++;                        
                        time = 0;
                    } 
                }
              
                if (spawnCount >= spawnCountForDecrease)
                {
                    spawnInterval *= timeDecreaseRate;
                    spawnInterval = Mathf.Clamp(spawnInterval, 0.1f, 1);
                    spawnCount = 0;
                }               
                yield return new WaitForEndOfFrame();
            } else
            {
                time += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            
        }
    }
}
