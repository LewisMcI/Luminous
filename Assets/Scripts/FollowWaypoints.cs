using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    public Transform[] waypoints;
    private Transform target;
    private int index = 0;
    private float minDistance = 0.1f;
    public float speed = 5;
    public bool canMove = true;

    public bool loop = true;

    private void Start()
    {
        target = waypoints[index];
    }
    private void LateUpdate()
    {
        if (canMove)
        {
            float movementStep = speed * Time.deltaTime;
            float distance = Vector3.Distance(transform.position, target.position);
            CheckDistanceToWaypoint(distance);
            transform.position = Vector3.MoveTowards(transform.position, target.position, movementStep);
        }
    }

    void CheckDistanceToWaypoint(float distance)
    {
        if (distance <= minDistance)
        {
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        index++;
        if (index == waypoints.Length)
        {
            if (loop == false)
            {
                Destroy(this.GetComponent<FollowWaypoints>());
            }
            index = 0;
        }
        target = waypoints[index];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
