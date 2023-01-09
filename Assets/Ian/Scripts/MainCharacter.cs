using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    walk, stop
}

public class MainCharacter : MonoBehaviour
{
    public MoveState state;
    public float walkSpeed;
    public float easeFactor;

    private Animator anim;
    private float actualSpeed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == MoveState.stop)
                transitionToWalk();
            else if (state == MoveState.walk)
                transitionToStop();
        }

        switch (state)
        {
            case MoveState.stop:
                if (actualSpeed > 0f)
                    finishWalk();
                break;
            case MoveState.walk:
                walk();
                break;
        }
    }

    private void walk()
    { 
        actualSpeed += Time.deltaTime * easeFactor;
        if (actualSpeed >= walkSpeed) actualSpeed = walkSpeed;
        transform.position += Vector3.left * actualSpeed * Time.deltaTime;
    }

    private void finishWalk()
    {
        actualSpeed -= Time.deltaTime * easeFactor;
        if (actualSpeed <= 0) actualSpeed = 0f;
        transform.position += Vector3.left * actualSpeed * Time.deltaTime;
    }

    private void transitionToWalk()
    {
        state = MoveState.walk;
        anim.SetTrigger("startWalk");
    }

    private void transitionToStop()
    {
        state = MoveState.stop;
        anim.SetTrigger("stopWalk");
    }
}
