using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interaction
{
    public KeyCode key;
    
    public bool isOpened;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(key))
        {
            if (!isOpened)
            {
                isOpened = true;
                anim.SetFloat("speed", 1f);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.01f)
                {
                    anim.Play("open", 0, 0f);
                }
            }
        }
        else if (Input.GetKeyUp(key))
        {
            if (isOpened) 
            { 
                isOpened = false;
                anim.SetFloat("speed", -1f);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                {
                    anim.Play("open", 0, 1f);
                }
            }
        }
        
    }
}
