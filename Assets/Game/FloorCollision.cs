using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollision : MonoBehaviour
{
    private MeshCollider mCollider;
    int playersOn = 0;
    private void Awake()
    {
        mCollider = (MeshCollider)GetComponent(typeof(MeshCollider));
        mCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Floor Sensor")
        {
            playersOn++;
            mCollider.isTrigger = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player Floor Sensor")
        {
            playersOn--;
            if (playersOn == 0)
            {
                mCollider.isTrigger = true;
            }

        }
    }
}