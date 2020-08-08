using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyTarget;
    public GameObject enemyPrefab;
    public List<Transform> targetSpawnPoints;
    public int spawnNumberPerRound = 5;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < targetSpawnPoints.Count; i++)
        {
            for(int j=0; j < spawnNumberPerRound; j++)
            {
                Instantiate(enemyPrefab, targetSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
