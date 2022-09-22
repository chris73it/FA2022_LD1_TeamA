using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    
    // Moving Values
    public float TopSpeed = 1f;
    public float Acceleration = 1f;
    public float Decceleration = 1f;
    private float _currentSpeed = 0f;

    // Moves Entity according to the transform
    public void Move(float horizontal, float vertical)
    {
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        controller.Move(direction * _currentSpeed * Time.deltaTime); 
    }

    // Updates Entity using its acceleration, decceleration, and topspeed
    public float UpdateSpeed(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            if (_currentSpeed + Acceleration < TopSpeed)
            {
                _currentSpeed += Acceleration;
            } else
            {
                _currentSpeed = TopSpeed;
            }
        } else
        {
            if (_currentSpeed - Decceleration >= 0) {
                _currentSpeed -= Decceleration;
            } else
            {
                _currentSpeed = 0;
            }
        }

        return _currentSpeed;
    }

}
