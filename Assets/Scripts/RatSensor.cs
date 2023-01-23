using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSensor : MonoBehaviour
{
    public Material ratMaterial;
    public RatSensor otherRatSensor;
    public GameObject ratSelected;

    public GameObject endPoint;

    MovementActivator maScript;
    private void Awake()
    {
        if (endPoint != null)
        {
            endPoint.SetActive(false);
        }
        else
        {
            Debug.Log("End Point not set!");
        }
        maScript = GetComponent<MovementActivator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player Sensor" && otherRatSensor.ratSelected != other.gameObject)
        {
            if (other.transform.parent.parent.GetComponent<Colour>() != null)
                if (other.transform.parent.parent.GetComponent<Colour>().cState == Colour.ColourState.Blue)
                {
                    ratSelected = other.gameObject;
                    GetComponent<Renderer>().material = ratMaterial;
                    maScript.canActivate = true;
                    if (otherRatSensor.ratSelected != null)
                    {
                        endPoint.SetActive(true);
                    }
                }
        }
    }
}
