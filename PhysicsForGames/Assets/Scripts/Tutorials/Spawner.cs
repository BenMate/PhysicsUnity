using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public Transform prefab;
   
    float timer = 0;
    bool shouldSpawn = false;

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn && timer >= 3.0f)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    public void SpawnZombie()
    {
        shouldSpawn = !shouldSpawn;
        Debug.Log(shouldSpawn);
    }
}
