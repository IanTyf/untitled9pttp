using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool isActive;

    public virtual void SetActive(bool b)
    {
        isActive = b;
    }

    public void Think()
    {
        Services.mainCharacter.Think();
    }

    public void StopThink()
    {
        Services.mainCharacter.StopThink();
    }
}
