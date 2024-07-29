//code basically part of assignment 1
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private void Update()
    {
        //only when mouse is moved send rays and display pos
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Blocks"))
                {
                    //update the position in ui 
                    GameObject.Find("GridPosition").gameObject.GetComponent<TextMeshProUGUI>().text = hit.transform.gameObject.transform.position.ToString();
                }
            }
        }
        
    }
}
