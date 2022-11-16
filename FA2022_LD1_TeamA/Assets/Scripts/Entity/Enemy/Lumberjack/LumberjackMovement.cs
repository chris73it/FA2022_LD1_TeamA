using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackMovement : EnemyMovement
{
    public float MaxHeight;
    public bool GoingUp;
    public float CurrentJumpSpeed = 0f;
    public Vector3 JumpTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + CurrentJumpSpeed, transform.position.z);

        if (transform.position.y >= MaxHeight)
        {
            CurrentJumpSpeed = 0f;
            GoingUp = false;
        } else
        {
            GoingUp = true;
        }
    }

    public float UpdateJumpSpeed()
    {
        if (GoingUp)
        {
            if (CurrentJumpSpeed + Acceleration < TopSpeed)
            {
                CurrentJumpSpeed += Acceleration;
            } else
            {
                CurrentJumpSpeed = TopSpeed;
            }
        } else
        {
            if (CurrentJumpSpeed - Decceleration >= -TopSpeed)
            {
                CurrentJumpSpeed -= Decceleration;
            } else
            {
                CurrentJumpSpeed = -TopSpeed;
            }
        }

        return CurrentSpeed;
    }

    public float
}
