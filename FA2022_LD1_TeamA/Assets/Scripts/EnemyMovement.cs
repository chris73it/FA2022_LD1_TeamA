using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public enum EnemyMovementTypes
    {
        Idle,
        Wander,
        Chasing
    }
    public float WanderRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Wander()
    {
        Horizontal = Random.Range(-1, 1);
        Vertical = Random.Range(-1, 1);
    }

    public void LocatePlayer()
    {

    }
}
