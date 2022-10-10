using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Rigidbody Body;
    public enum EnemyMovementTypes
    {
        Idle,
        Wander,
        /*
        Chase,
        Attack
        */
    }
    public EnemyMovementTypes Type = EnemyMovementTypes.Wander;
    public float StateTimer = 0f;
    public float SightRange = 30f;

    // Update is called once per frame
    void Update()
    {
        if (StateTimer <= 0)
        {
            ChooseNewState();
            switch (Type)
            {
                case (EnemyMovementTypes.Idle):
                    Idle();
                    StateTimer = 5f;
                    break;

                case (EnemyMovementTypes.Wander):
                    Wander();
                    StateTimer = 3f;
                    break;
            }
        } else
        {
            StateTimer -= Time.deltaTime;
        }
        //Debug.Log("CurrentSpeed: " + CurrentSpeed);
        UpdateSpeed();
        Move();
    }

    public virtual void Idle()
    {
        Horizontal = 0;
        Vertical = 0;
    }

    public virtual void Wander()
    {
        //cannot think of a better way to do this...
        float r = 0;

        while (r == 0)
        {
            r = Random.Range(-1, 2);

        }

        Horizontal = r; 

        while (r == 0)
        {
            r = Random.Range(-1, 2);

        }

        Vertical = r = Random.Range(-1f, 1f);
        Debug.Log("Horizontal: " + Horizontal);
        Debug.Log("Vertical: " + Vertical);
    }

    public virtual void Chase()
    {
        Vector3 playerLocation = GameManager.ChosenPlayerCharacter.transform.position;

        if (playerLocation.x < transform.position.x)
        {
            Horizontal = Random.Range(-1, 0);
        } else
        {
            Horizontal = Random.Range(0, 1);
        }

        if (playerLocation.z < transform.position.z)
        {
            Vertical = Random.Range(-1, 0);
        }
        else
        {
            Vertical = Random.Range(0, 1);
        }
    }

    public void ChooseNewState()
    {
        int statesLength = EnemyMovementTypes.GetNames(typeof(EnemyMovementTypes)).Length;
        EnemyMovementTypes newState = Type;

        while (newState == Type)
        {
            newState = (EnemyMovementTypes)Random.Range(0, statesLength);
        }

        Type = newState;
        Debug.Log("Type: " + Type);
    }

    public override void Move()
    {
        Vector3 direction = transform.right * Horizontal + transform.forward * Vertical;
        Body.MovePosition(transform.position + direction * CurrentSpeed * Time.deltaTime);
    }
}
