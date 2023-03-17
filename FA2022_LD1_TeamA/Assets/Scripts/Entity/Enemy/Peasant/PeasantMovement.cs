using UnityEngine;
using UnityEngine.AI;

public class PeasantMovement : EnemyMovement
{
    public enum PeasantStates
    {
        Idle,
        Wander,
        Pursuit,
        Attacking
    }

    private PeasantStates state = PeasantStates.Idle;
    public PeasantStates State
    {
        get
        {
            return state; 
        }
        set 
        { 
            state = value;
            switch (state)
            {
                case (PeasantStates.Idle):
                    NavMeshAgent.speed = 0;
                    NavMeshAgent.acceleration = -1000;
                    Combat.Animator.SetTrigger("Idle");
                    Debug.Log("Idle");
                    break;

                case (PeasantStates.Wander):
                    Combat.Animator.SetTrigger("Walking");
                    break;

                case (PeasantStates.Pursuit):
                    Combat.Animator.SetTrigger("Walking");
                    break;

                default:
                    break;
            }
        }
    }
   
    public float WanderRange = 2f;
    public bool Wandering = false;
    public bool Pursuing = false;
    public PeasantCombat Combat;

    public override void Initialize()
    {
        base.Initialize();
        Combat = GetComponent<PeasantCombat>();
    }

    private void Update()
    {
        if (StateTimer <= 0f)
        {
            if (State == PeasantStates.Wander)
            {
                if (!Wandering)
                {
                    Wander();
                }

                if (transform.position.x < Destination.x)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }

                Move();

                Debug.Log("WanderMovement");

                if (NavMeshAgent.remainingDistance <= 0)
                {
                    Wandering = false;
                    StateTimer = 6f;
                    State = PeasantStates.Idle;
                }
            }
        } else
        {
            StateTimer -= Time.deltaTime;
        }
        
        if (State == PeasantStates.Pursuit)
        {
            if (!Pursuing)
            {
                Pursuit();
                Wandering = false;
            }

            if (Pursuing)
            {
                Destination = Player.transform.position;
            }

            if (transform.position.x < GameManager.ChosenPlayerCharacter.transform.position.x)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            }

            Move();
        }

        if (State != PeasantStates.Pursuit)
        {
            Pursuing = false;
        }

        if (State == PeasantStates.Attacking)
        {
            SetNavMeshSpeed(0f);
        }
    }

    public void Wander()
    {
        SetNavMeshSpeed(1f);
        Vector3 randomPosition = Random.insideUnitSphere * WanderRange;
        randomPosition += transform.position;
        Wandering = true;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, WanderRange, 1);

        Destination = hit.position;
        //Debug.Log(transform.position);
    }

    public void Pursuit()
    {
        SetNavMeshSpeed(2f);
        Pursuing = true;
    }

    public override void Move()
    {
        base.Move();
        Debug.Log("Animated");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (State == PeasantStates.Wander)
            {
                Wander(); // maybe there should be a better failsafe for this or change NavMeshAgent.remainingDistance <= 0 to something else
            }
        }
    }
}
