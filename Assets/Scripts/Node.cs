using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3 Position;
    public bool isWalkable = true; // if element of obstacleToggled is 0 then its walkable
    public float costFromStart;// cost from start to current node
    public float costToEnd;//cost from current to end (this will keep changing as we move)
    public float totalCost => costFromStart + costToEnd;

    public Node parent; // parent node to retrace it to starting point

    public Node(Vector3 position, bool isWalkable)
    {
        Position = position;
        this.isWalkable = isWalkable;

    }
}
