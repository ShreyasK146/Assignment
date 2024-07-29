//grid or obstacle prefab are sketchfab
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject block;
    [HideInInspector] public Vector3[] blockPos = new Vector3[100];

    int rows = 10;
    int columns = 10;

    private void Start()
    {
        GenerateGrid();//to generate a base grids/map
    }

    private void GenerateGrid()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                //instantiate block side by side multiply x and z by 2 because block size is 1,1,1
                GameObject newBlock = Instantiate(block,new Vector3(j*2,0,i*2),Quaternion.identity);
                blockPos[i * 10 + j] = new Vector3(j * 2,2f,i * 2);
                
            }
        }
    }
    

}
