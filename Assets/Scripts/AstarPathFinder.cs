/*
 * taken help from Unity AI Course on Udemy
 * https://www.udemy.com/course/artificial-intelligence-in-unity/?couponCode=MCLARENT71824
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AstarPathFinder : MonoBehaviour
{
    [SerializeField] ObstacleData obstacle;
    Node[,] grid = new Node[10,10]; 
    private void Start()
    {
        InitializeGrid();//to initialize the grids based on the obstacles and base grid/land we generated
    }

    private void InitializeGrid()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 position = new Vector3(j * 2, 0, i * 2); // adjust the spacing * 2
                bool isWalkable = !(obstacle.obstacleToggled[i * 10 + j]);// if 1 then obstacle is there so not walkable
                grid[i, j] = new Node(position, isWalkable); // becomes walkable node/grid if there's no obstacle present
            }

        }
    }


    public Node GetNodeAtPos(Vector3 pos) => grid[(int)pos.z/2, (int)pos.x/2];


    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        
        Node startNode = GetNodeAtPos(startPos);
        Node endNode = GetNodeAtPos(targetPos);
     

        List<Node> openSet = new List<Node>() { startNode }; // these are the grids we are yet to trace path and we initialize the first node here
        List<Node> closedSet = new List<Node>(); // these are the nodes or grids we are already traced path 

        //Debug.Log(openSet.Count + " " + closedSet.Count);
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            // everytime we start choosing a grid/node we need to choose the one which has less total cost or costToend should be less 
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].totalCost < currentNode.totalCost ||
                    openSet[i].totalCost == currentNode.totalCost && openSet[i].costToEnd < currentNode.costToEnd)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }
            // finding the neighbours from current node 
            foreach(Node neighbor in GetNeighbors(currentNode))
            {
                
                if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                float costToNewNeighbor =  currentNode.costFromStart + GetDistance(currentNode, neighbor); // cost from start + cost from start to neighbor
                if(costToNewNeighbor < currentNode.costFromStart || !openSet.Contains(neighbor))
                {
                    neighbor.costFromStart = costToNewNeighbor; // cost from start for our neighbor
                    neighbor.costToEnd = GetDistance(neighbor, endNode); // from the selected neighbor to end node we calculate distance
                    neighbor.parent = currentNode; // essential to back track so we keep track of previous node of current node
                    
                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        
        return null;
    }


    //once you get the shortest path u need to trace it back to the starting node
    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node>path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode) 
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse(); // basically we need the path from start to end so just reverse the backtracked path
        //Debug.Log(path.Count);
        return path;
    }



    // we need neighbours from current node where we dont have obstacles to move to target if not target is not reached 
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        int x = (int)node.Position.x / 2;
        int z = (int)node.Position.z / 2;

        // Define the directions for up, down, left, right
        int[] dx = { -1, 1, 0, 0 };
        int[] dz = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int checkX = x + dx[i];
            int checkZ = z + dz[i];

            if (checkX >= 0 && checkX < 10 && checkZ >= 0 && checkZ < 10)
            {
                neighbors.Add(grid[checkZ, checkX]);
            }
        }

        //Debug.Log($"Node at {node.Position} has {neighbors.Count} neighbors:");
        //foreach (var neighbor in neighbors)
        //{
        //    Debug.Log($"Neighbor at {neighbor.Position} isWalkable: {neighbor.isWalkable}");
        //}

        return neighbors;
    }

    // to basically calculate distance from one node another node (here we wont consider diagonal only 4 direction)
    private float GetDistance(Node nodeA, Node nodeB) =>
        Mathf.Abs(nodeA.Position.x - nodeB.Position.x) + Mathf.Abs(nodeA.Position.z - nodeB.Position.z);
     
    
   
}