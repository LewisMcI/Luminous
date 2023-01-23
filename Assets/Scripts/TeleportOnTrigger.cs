using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{

    public Vector3 positionToTelportTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = positionToTelportTo;
        }
    }
}
