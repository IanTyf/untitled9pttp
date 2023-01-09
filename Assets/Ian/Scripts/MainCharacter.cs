using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    walk, stop, think
}

public class MainCharacter : MonoBehaviour
{
    public MoveState state;
    public float walkSpeed;
    public float easeFactor;

    private Animator anim;
    private float actualSpeed;

    private void Awake()
    {
        Services.mainCharacter = this;
    }

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
                StartCoroutine(transitionToWalk());
            else if (state == MoveState.walk)
                StartCoroutine(transitionToStop());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (state == MoveState.walk || state == MoveState.stop)
                StartCoroutine(transitionToThink());
            else if (state == MoveState.think)
                StartCoroutine(transitionToWalk());
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
            case MoveState.think:
                think();
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

    private void think()
    {
        
    }

    IEnumerator transitionToWalk()
    {
        anim.SetTrigger("startWalk");
        while (transform.eulerAngles.y > 270f || transform.eulerAngles.y == 0f)
        {
            transform.localEulerAngles -= Vector3.up * 70f * Time.deltaTime;
            if (transform.localEulerAngles.y < 270f) transform.localEulerAngles = new Vector3(0f, 270f, 0f);
            yield return null;
        }
        state = MoveState.walk;
    }

    IEnumerator transitionToStop()
    {
        state = MoveState.stop;
        while (transform.eulerAngles.y > 270f || transform.eulerAngles.y == 0f)
        {
            transform.localEulerAngles -= Vector3.up * 70f * Time.deltaTime;
            if (transform.localEulerAngles.y < 270f) transform.localEulerAngles = new Vector3(0f, 270f, 0f);
            yield return null;
        }
        anim.SetTrigger("startStop");
    }

    IEnumerator transitionToThink()
    {
        anim.SetTrigger("startThink");
        state = MoveState.think;
        while (transform.eulerAngles.y >= 270f)
        {
            transform.localEulerAngles += Vector3.up * 70f * Time.deltaTime;
            if (transform.localEulerAngles.y < 270f) transform.localEulerAngles = Vector3.zero;
            yield return null;
        }
    }

    public void Think()
    {
        if (state != MoveState.think)
        {
            StartCoroutine(transitionToThink());
        }
    }

    public void StopThink()
    {
        if (state == MoveState.think)
        {
            StartCoroutine(transitionToWalk());
        }
    }
}
