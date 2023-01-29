using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiManager : MonoBehaviour
{
    public List<AnyInput> anyInputs = new List<AnyInput>();

    private void Awake()
    {
        Services.aiManager = this;
    }

    public void Add(AnyInput ip)
    {
        if (!anyInputs.Contains(ip))
        {
            Debug.Log("added new anyinput");
            anyInputs.Add(ip);
        }
    }

    // if any input gets decided and triggered, let all other inputs know and reset their listener
    public void ClearListeners()
    {

    }
}
