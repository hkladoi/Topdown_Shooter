using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float nextWaypointDistance;
    public float repeatTimeUpdatePath = 0.5f;
    public SpriteRenderer enemySR;
    public Seeker seeker;
    Transform target;
    Path path;
    Coroutine moveCoroutine;

    void Start()
    {
        target = FindAnyObjectByType<Player>().gameObject.transform;
        //CalulatePath();
        InvokeRepeating("CalulatePath", 0f, repeatTimeUpdatePath);
    }


    void CalulatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            MoveToTarget();
        }

    }

    void MoveToTarget()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWaypoint = 0;
        while (currentWaypoint < path.vectorPath.Count)
        {
            Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
            Vector2 force = dir * speed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            if (force.x != 0)
            {
                if (force.x < 0)
                {
                    enemySR.transform.localScale = new Vector3(-1, 1, 0);
                }
                else
                {
                    enemySR.transform.localScale = new Vector3(1, 1, 0);
                }
            }
            yield return null;
        }
    }
}
