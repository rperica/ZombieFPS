using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 playerPosition;
    private Pathfinding pathfinder;

    private Vector3[] pathWaypoints;
    private Vector3 targetWayPoint;

    private int targetIndex = 0;

    public float enemySpeed = 2.0f;

    private void Start()
    {
        pathfinder = EnemyManager.Instance.GetComponent<Pathfinding>();      
    }

    private void Update()
    {
        playerPosition = EnemyManager.Instance.playerTransform.position;

        pathWaypoints = pathfinder.PathFinding(transform.position, playerPosition);

        if(pathWaypoints!=null)
            MoveToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.DefaultState();
        GameManager.Instance.LoadScene(SceneID.GameOverScene);
    }

    private void MoveToPlayer()
    {
        if (targetIndex < pathWaypoints.Length)
        {
            targetWayPoint = pathWaypoints[targetIndex];

            if (targetIndex == (pathWaypoints.Length - 1))
            {
                targetWayPoint = playerPosition;
            }

            transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint - transform.position, enemySpeed * Time.deltaTime, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, enemySpeed * Time.deltaTime);

            if (transform.position == targetWayPoint)
            {
                targetIndex++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (pathWaypoints != null)
        {
            foreach (Vector3 n in pathWaypoints)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(n, Vector3.one * (0.9f));
            }
        }
    }
}
