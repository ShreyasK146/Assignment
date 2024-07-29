/*
 * Taken help from below YT to code this part of the code
 * https://youtu.be/Ub9UhAibqPg
 */



using System;
using UnityEditor;
using UnityEngine;

public class ObstacleEditor : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Window/Obstacle Editor")]
    public static void ShowWindow()
    {
        //to show the basic window and set it size 
        ObstacleEditor win = (ObstacleEditor)GetWindow(typeof(ObstacleEditor));
        win.maxSize = new Vector2(500, 500);
        win.minSize = new Vector2(500, 500);
        
    }

    private void OnEnable()
    {  
        string pathToScriptableObject = "Assets/Scripts/Obstacles.asset";
        obstacleData = AssetDatabase.LoadAssetAtPath<ObstacleData>(pathToScriptableObject); //assigns my scriptable object's object or asset from project folder
    }

    private void OnGUI()    
    {
        //initialize array
        for(int i = 0; i < 10; i++)
        {
            //making sure each row has 10 box
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 10; j++)
            {
                //formula to find element in 1D array from 2D = rows * width + column
                int index = i * 10 + j; 
                //bool array of scriptable object is initialized based on editor values
                obstacleData.obstacleToggled[index] = EditorGUILayout.Toggle(obstacleData.obstacleToggled[index],GUILayout.Width(25),GUILayout.Height(25));
                EditorUtility.SetDirty(obstacleData);//we need to add this for some reason because unity does not save values properly ? ??
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
