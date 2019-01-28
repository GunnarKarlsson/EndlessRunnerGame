using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    private enum CameraState
    {
        INTRO,
        FOLLOW
    }

    private Transform lookAt;
    public Vector3 startOffset;
    private Vector3 moveVector;
    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    public Vector3 animationStartPosition = new Vector3(0f,5f,-5f);
    private CameraState state;

    void Start()
    {
        state = CameraState.INTRO;
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    void Update()
    {
        moveVector = lookAt.position + startOffset;
        moveVector.x = 0f;
        switch(state)
        {
            case CameraState.FOLLOW:
                Follow();
                break;

            case CameraState.INTRO:
                DoIntroAnimation();
                break;
        }


    }

    void Follow()
    {
        transform.position = moveVector;
    }

    void DoIntroAnimation()
    {
        transform.position = Vector3.Lerp(moveVector + animationStartPosition, moveVector, transition);
        //transform.LookAt(lookAt.position + Vector3.up);

        transition += Time.deltaTime / animationDuration;
        if (transition > 1.0f)
        {
            state = CameraState.FOLLOW;
        }
    }
}