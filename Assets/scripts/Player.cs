using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    public PlayerAnimationController playerAnimationController;
    private bool isPunching = false;
    private bool isPointingLeftDirection = false;
    void Start()
    {
        // transform.position = new Vector3(-0.3f, -0.3f, 0.0f);
    }

    void Update()
    {

        HandleInputAction();
        HandleHorizontalAxisListener();

    }

    void HandleHorizontalAxisListener()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (!isPunching)
        {
            HandleInputDirection(horizontalInput);

        }

        Vector3 movement = new Vector3(horizontalInput, -0.0f, 0.0f);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (transform.position.x < -8f)
        {
            transform.position = new Vector3(-8f, -3f, 0);
        }
    }

    void HandleInputDirection(float input)
    {
        if (input > 0)
        {
            PlayAnimation("WalkRight");
            isPunching = false;
            isPointingLeftDirection = false;
        }
        else if (input < 0)
        {
            PlayAnimation("WalkLeft");
            isPunching = false;
            isPointingLeftDirection = true;
        }
        if (input == 0)
        {
            if (isPointingLeftDirection)
            {
                PlayAnimation("IdleLeft");
            }
            else
            {
                PlayAnimation("Idle");
            }
        }
    }

    void HandleInputAction()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isPointingLeftDirection && !isPunching)
            {
                PlayAnimation("PunchLeft");
            }
            else
            {
                PlayAnimation("PunchRight");
            }
            isPunching = true;
            StartCoroutine(SetIsPunchFalse(0.5f));
        }
    }

    private IEnumerator SetIsPunchFalse(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPunching = false;
    }

    void PlayAnimation(string animationName)
    {
        playerAnimationController.PlayAnimation(animationName);
    }

}
