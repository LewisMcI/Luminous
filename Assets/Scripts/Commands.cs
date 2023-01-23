using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    public bool reachedWaypoint = false;

    public bool ReachedWaypoint()
    {
        return this.reachedWaypoint;
    }
    public void SlowlyMoveTo(GameObject objToMove, Vector3 posToGoto, float speed)
    {
        reachedWaypoint = false;
        StartCoroutine(MoveOverTime(objToMove, posToGoto, speed));
    }

    private IEnumerator MoveOverTime(GameObject obj, Vector3 pos, float speed)
    {
        yield return new WaitForSeconds(0.1f);
        float distance = Vector3.Distance(obj.transform.localPosition, pos);
        Vector3 dir = pos - obj.transform.localPosition;

        float finalTime = Time.time + 1.0f;
        for (float currentTime = Time.time; currentTime < finalTime; currentTime += Time.deltaTime)
        {
            obj.transform.localPosition += dir * (speed * Time.deltaTime);
        }
        this.reachedWaypoint = true;
    }

}
