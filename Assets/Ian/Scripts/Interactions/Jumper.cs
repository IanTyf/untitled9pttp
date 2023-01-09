using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Interaction
{
    public KeyCode key;

    public Door doorInteraction;

    private Animator anim;
    private bool isInside;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (Input.GetKey(key))
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyDown(key))
        {
            anim.ResetTrigger("holdFail");
            anim.SetTrigger("hold");
        }
        else if (Input.GetKeyUp(key))
        {
            if (timer > 2f)
            {
                // jump
                anim.Play("jump");
            }
            else
            {
                // jump failed
                anim.SetTrigger("holdFail");
            }

            anim.ResetTrigger("hold");
            timer = 0f;
        }
    }

    public void CheckForDoor()
    {
        if (doorInteraction.isOpened)
        {
            // go inside
            anim.Play("goInside");
        }
        else
        {
            // leave
            anim.Play("leave");
        }
    }
}
