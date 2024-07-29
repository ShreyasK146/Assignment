using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] ObstacleData obstacleData;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GridGenerator gridGenerator;
    private bool enemySpawned = false;
    private bool playerSpawned = false;
    int randomHolder;
    private void Start()
    {
        Invoke("SpawnPlayerandEnemy", 0.5f);

    }

    private void SpawnPlayerandEnemy()
    {
        // spawn player and enemy at random pos where obstacle is present
        while(!playerSpawned)
        {
            int random = Random.Range(0, 100);
            if (!obstacleData.obstacleToggled[random])
            {
                Instantiate(player, gridGenerator.blockPos[random], Quaternion.identity);
                obstacleData.obstacleToggled[random] = true; // making sure enemy does not spawn above player
                randomHolder = random;
                playerSpawned = true;   
            }
        }
        while (!enemySpawned)
        {
            int random = Random.Range(0, 100);
            if (!obstacleData.obstacleToggled[random])
            {
                Instantiate(enemy, gridGenerator.blockPos[random], Quaternion.identity);
                enemySpawned = true;
            }
        }
        obstacleData.obstacleToggled[randomHolder] = false;// make the randomHolder to false because this block basically has moving object that is player
    }
}
