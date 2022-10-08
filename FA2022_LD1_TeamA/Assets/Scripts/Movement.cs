using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
    // Moving Values
    public float TopSpeed = 1f;
    public float Acceleration = 1f;
    public float Decceleration = 1f;
    public float CurrentSpeed = 0f;
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    // Moves Entity according to the transform
    public virtual void Move()
    {
        Debug.Log("Base Move Method");
    }

    // Updates Entity using its acceleration, decceleration, and topspeed
    public float UpdateSpeed()
    {
        if (Horizontal != 0 || Vertical != 0)
        {
            if (CurrentSpeed + Acceleration < TopSpeed)
            {
                CurrentSpeed += Acceleration;
            } else
            {
                CurrentSpeed = TopSpeed;
            }
        } else
        {
            if (CurrentSpeed - Decceleration >= 0) {
                CurrentSpeed -= Decceleration;
            } else
            {
                CurrentSpeed = 0;
            }
        }

        return CurrentSpeed;
    }

}
