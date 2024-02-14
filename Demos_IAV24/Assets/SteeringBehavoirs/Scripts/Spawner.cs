using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static int numEnemy = 0;

    public static int maxEnemy = 10;

    public GameObject prefab = null;
    public Transform parent = null;

    public float spawnDelay = 5.0f;

    public Transform target = null;

    private float lastSpawn = 0.0f;


    
    
    // Start is called before the first frame update
    void Start()
    {
        lastSpawn =  spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxEnemy != 0 && numEnemy < maxEnemy)
        {
            lastSpawn += Time.deltaTime;

            if(lastSpawn >= spawnDelay)
            {
                Spawner.numEnemy++;

                GameObject newGO = Instantiate(prefab, transform.position, Quaternion.identity,parent);
                
                lastSpawn = 0.0f;
            }
        }
    }
}
