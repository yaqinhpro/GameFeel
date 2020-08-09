using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyTarget;
    public GameObject enemyPrefab;
    public List<Transform> targetSpawnPoints;
    public int spawnNumPerRound = 5;
    public int oneRoundTime = 5;

    private int totalActiveEnemiesNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemies", 0f, 3f);
    }

    private void CreateEnemies()
    {
        int actualSpawnNumPerRound = Random.Range(0, spawnNumPerRound + 1);

        for (int i = 0; i < targetSpawnPoints.Count; i++)
        {            
            for (int j = 0; j < actualSpawnNumPerRound; j++)
            {
                Enemy enemyScript = Instantiate(enemyPrefab, targetSpawnPoints[i].position, Quaternion.identity).GetComponent<Enemy>();
                enemyScript.target = enemyTarget;
                enemyScript.enemySpawner = this;

                totalActiveEnemiesNum += 1;
            }
        }
    }

    public void EnemyDied()
    {
        if (totalActiveEnemiesNum > 0)
        {
            totalActiveEnemiesNum -= 1;
        }
    }
}
