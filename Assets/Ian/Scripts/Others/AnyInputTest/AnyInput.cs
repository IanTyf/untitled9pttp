using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyInput : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("adding new anyinput");
        Services.aiManager.Add(this);
    }

    public virtual void Listen()
    {
        
    }

    public virtual void Trigger()
    {
        Services.aiManager.ClearListeners();
    }
}
