using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public float platCount = 0f;
    public float spikeCount = 0f;
    public float movCount = 0f;
    public float breakCount = 0f;
    
    
    
    public GameObject platformPrefab;
    public GameObject spikePlatformPrefab;
    public GameObject[] movingPlatforms;
    public GameObject breakablePlatform;

    public float platform_Spawn_Timer = 2f;
    private float current_Platform_Spawn_Timer;

    private int platform_Spawn_Count;

    public float min_X = -2f, max_X = 2f;
    void Start()
    {
        current_Platform_Spawn_Timer = platform_Spawn_Timer;
    }

    void Update()
    {
        SpawnPlatforms();
    }

    void SpawnPlatforms()
    {
        current_Platform_Spawn_Timer += Time.deltaTime;

        if (current_Platform_Spawn_Timer >= platform_Spawn_Timer)
        {
            platform_Spawn_Count++;
            var temp = transform.position;
            temp.x = Random.Range(min_X, max_X);
            GameObject newPlatform = null;

            if (platform_Spawn_Count < 2) {
                newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);                                     
                platCount++;
            }else if (platform_Spawn_Count == 2)
            {
                
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);                                 
                    platCount++;
                }
                else
                {
                    newPlatform = Instantiate(movingPlatforms[Random.Range(0, movingPlatforms.Length)], temp,
                        Quaternion.identity);
                    movCount++;
                }
            }
            else if (platform_Spawn_Count == 3)
            {
                
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);                             
                    platCount++;
                }
                else
                {
                    newPlatform = Instantiate(spikePlatformPrefab, temp, Quaternion.identity);
                    spikeCount++;
                }
            }else if (platform_Spawn_Count == 4)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);                       
                    platCount++;
                }
                else
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                    breakCount++;
                }
                platform_Spawn_Count = 0;
            }

            newPlatform.transform.parent = transform;
            current_Platform_Spawn_Timer = 0;
            //Count();
        }
    }//spawn platform

    void Count()
    {
        float sum = 0;
        sum = platCount + breakCount + movCount + spikeCount;

        var platCount2 = (platCount / sum) * 100;
        var movCount2 = (movCount / sum) * 100;
        var spikeCount2 = (spikeCount / sum) * 100;
        var breakCount2 = (breakCount / sum) * 100;
        
        print("Normal Platform: " + platCount2 + "%"); //63%
        print("Movable Platform: " + movCount2 + "%"); //13%
        print("Spike Platform: " + spikeCount2 + "%"); //12%
        print("Breakable Platform: " + breakCount2 + "%"); //12%
    }
}

