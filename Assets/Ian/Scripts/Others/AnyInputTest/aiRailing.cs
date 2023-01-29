using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiRailing : AnyInput
{
    private string storedKey;
    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Listen();
    }

    public override void Listen()
    {
        base.Listen();

        string inputS = Input.inputString;

        // if no key is pressed
        if (storedKey.Length == 0)
        {
            if (inputS.Length == 1) storedKey = inputS.Trim().ToLower();
        }
        // if a key is pressed
        else
        {
            // check if key is released
            if (Input.GetKeyUp(storedKey))
            {
                
            }
            else
            {
                
            }
        }
    }

    public override void Trigger()
    {
        base.Trigger();

        // do something
    }
}
