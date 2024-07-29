using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] ObstacleData obstacleData;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GridGenerator gridGenerator;
    private void Start()
    {
        Invoke("GenerateObstacle", 0.1f);
    }

    //basically generate obstacles where we have 1's in obstacleToggled bool array 
    private void GenerateObstacle()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int k = i * 10 + j;
                if (obstacleData.obstacleToggled[k])
                {
                    Instantiate(obstaclePrefab, gridGenerator.blockPos[k], Quaternion.identity);
                }
            }

        }

    }
}
