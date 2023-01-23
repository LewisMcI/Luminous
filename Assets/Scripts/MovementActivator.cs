using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementActivator : MonoBehaviour
{
    public FollowWaypoints fwScript;
    public bool invert = false;
    public bool canActivate = true;
    public Colour.ColourState cRequired;
    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(transform.position, other.transform.position) <= 5.0f && canActivate)
        {
            if (other.transform.parent.parent.GetComponent<Colour>() != null)
                if (other.transform.parent.parent.GetComponent<Colour>().cState == cRequired)
                {
                    if (other.gameObject.tag == "Player Sensor")
                    {
                        if (invert == true)
                        {
                            Debug.Log("Can Move");
                            fwScript.canMove = true;
                        }
                        else
                        {
                            Debug.Log("Can't Move");
                            fwScript.canMove = false;
                        }
                    }
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Vector3.Distance(transform.position, other.transform.position) <= 5.0f && canActivate)
        {
            if (other.gameObject.tag == "Player Sensor")
            {
                if (invert == true)
                {
                    Debug.Log("Can't Move");
                    fwScript.canMove = false;
                }
                else
                {
                    Debug.Log("Can Move");
                    fwScript.canMove = true;
                }
            }
        }
    }
}
