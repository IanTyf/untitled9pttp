using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 targetPos = target.transform.position - offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
    }

    public void SetTarget(GameObject t)
    {
        target = t;
        offset = t.transform.position - transform.position;
    }

    public void StopFollow()
    {
        target = null;
        offset = Vector3.zero;
    }
}
