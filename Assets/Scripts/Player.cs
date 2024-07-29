/*Animation and character is taken from mixamo*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AstarPathFinder pathFinder;
    private List<Node> path;
    public float speed = 5f;
    private Animator anim;
    public bool pathReached = true; // basically to control the multiple clicking and helps enemy to start following
    
    private void Start()
    {
        pathFinder = GameObject.Find("Astar").GetComponent<AstarPathFinder>();
        anim = GetComponent<Animator>();
        if (pathFinder == null)
        {
            Debug.LogError("AstarPathFinder not found in the scene.");
        }
    }

    private void Update()
    {
        //when the mouse button on is clicked player should calcualate the path, retrieve the path and start moving 
        if (Input.GetMouseButtonDown(0) && pathReached)// only when path is reached we are allowed take a input from mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
                if (hit.transform.CompareTag("Blocks"))
                {
                    Vector3 targetPos = hit.transform.gameObject.transform.position;
                    List<Node> path = pathFinder.FindPath(transform.position, targetPos);
                    if(path.Count > 0 && path!=null && pathReached)
                    {
                        pathReached = false; 
                    }
                    if (path != null && !pathReached)
                    {
                        StopCoroutine("FollowPath");
                        StartCoroutine("FollowPath", path);

                    }

                }
            }
        }
    }

    private IEnumerator FollowPath(List<Node> path)
    {
        if(!anim.GetBool("walking"))
        {
            //anim.SetTrigger("walking1");
            anim.SetBool("walking",true);
        }

        foreach (Node node in path)
        {
            Vector3 targetPos = new Vector3(node.Position.x, transform.position.y, node.Position.z);
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                // to see in the direction of its forward vector while moving
                Vector3 direction = (targetPos - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;// calculate the angle betwee x and z and rotate 
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }
        }
        //anim.SetTrigger("idle");
        anim.SetBool("walking", false);
        pathReached = true;
    }
}
