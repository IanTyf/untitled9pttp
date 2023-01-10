using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooftop : Interaction
{
    public KeyCode[] keys;

    private Animator anim;
    private int prevInd;
    private int correctCount;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        prevInd = -2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("birdFlyAndDissapoint")) return;

        int pressedKey = -1;
        if (Input.GetKeyDown(keys[0])) pressedKey = 0;
        if (Input.GetKeyDown(keys[1])) pressedKey = 1;
        if (Input.GetKeyDown(keys[2])) pressedKey = 2;

        if (pressedKey == -1)
        {
            //nothing pressed
        }
        else
        {
            if (pressedKey - prevInd == 1)
            {
                //correct key
                correctCount++;
            }
            else
            {
                //wrong key
                correctCount = 0;
                if (pressedKey != 0) anim.SetTrigger("wrongFly");
            }
            prevInd = pressedKey;
        }

        if (correctCount == 2)
        {
            anim.SetTrigger("flyAwaySad");
            correctCount = 0;
        }
    }
}
