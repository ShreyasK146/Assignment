using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacles", menuName = "Obstacle/CreateObstacle")] 
public class ObstacleData : ScriptableObject
{
    // this array holds the editor values;
    [HideInInspector]public bool[] obstacleToggled = new bool[100];
}
