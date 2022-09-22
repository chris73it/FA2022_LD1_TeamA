using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private float _horizontal;
    private float _vertical;

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        UpdateSpeed(_horizontal, _vertical);
        Move(_horizontal, _vertical);
    }
}
