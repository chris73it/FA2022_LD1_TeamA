using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public CharacterController Controller;

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        UpdateSpeed();
        Move();
    }

    public override void Move() {
        Vector3 direction = transform.right * Horizontal + transform.forward * Vertical;
        Controller.Move(direction * CurrentSpeed * Time.deltaTime);
    }
}
