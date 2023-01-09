using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowTrigger : MonoBehaviour
{
    public CameraFollow cf;
    public bool isStop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isStop)
            {
                cf.StopFollow();
            }
            else
            {
                cf.SetTarget(other.gameObject);
            }
        }
    }
}
