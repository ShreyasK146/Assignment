
/*
 * The following part of assignment 4 is not working properly
 * 
 Once it reaches the desired tile, the unit should stay still until the player unit moves. This should be done following proper OOP concepts. The ‘Enemy AI’ script 
 is expected to inherit from an ‘AI’ interface. 
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player; 
    private AstarPathFinder pathFinder;
    private List<Node> path;
    public float speed = 4f;
    private Animator anim;
    public bool isFollowing = false;


    private void Start()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        pathFinder = GameObject.Find("Astar").GetComponent<AstarPathFinder>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isFollowing && player.GetComponent<Player>().pathReached)
        {
            // if not following get player pos find path and start follow
            Vector3 playerPosition = player.position;
            path = pathFinder.FindPath(transform.position, playerPosition);
           
            if (path != null && path.Count > 1)
            {
                isFollowing = true;
                StartCoroutine(FollowPath(path));
            }
        }
    }

    private IEnumerator FollowPath(List<Node> path)
    {
        if (!anim.GetBool("walking"))
        {
            //anim.SetTrigger("walking1");
            anim.SetBool("walking", true);
        }
        for(int i =  0; i < path.Count ; i++)//tried to keep the enemy one block away from player by taking path.Count - 1(not working properly)
            //wierdly sometimes the enemy moves back and forth..
        {
            //same as player get target and move towards it
            Vector3 targetPos = new Vector3(path[i].Position.x, transform.position.y, path[i].Position.z);
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                // to see in the direction of its forward vector while moving
                Vector3 direction = (targetPos - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null;
            }
        }
        isFollowing = false;
        //anim.SetTrigger("idle");
        anim.SetBool("walking", false);
     
    }
}