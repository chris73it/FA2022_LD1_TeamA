using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedcapFairyMovement : EnemyMovement
{
    public enum RedcapFairyStates
        {
            Idle,
            Join,
            Wander,
            Pursuit,
            Attacking,
        }

    public RedcapFairyStates State = RedcapFairyStates.Join;
    private bool joining = false;
    private bool wandering = false;
    private bool pursuing = false;

    public static List<Vector3> StartPositions;
    public List<Vector3> ClusterPositions;
    public float ClusterWanderRange = 3f;
    public float ClusterCheckSize = 20f;
    public RedcapFairyCombat Combat;



    // Add position to StartPositions
    public void Start()
    {
        StartPositions.Add(transform.position);
    }

    // Intialize
    public override void Initialize()
    {
        base.Initialize();
        Combat = GetComponent<RedcapFairyCombat>();
        StartPositions = new List<Vector3>();
        ClusterPositions = new List<Vector3>();
        NavMeshAgent.stoppingDistance = 0.1f;
    }

    // Join State logic
    public void Join()
    {
        Vector3 average = new Vector3(0, 0, 0);

        foreach (Vector3 v in StartPositions)
        {
            average += v;
        }

        average += transform.position;

        average /= StartPositions.Count + 1;

        Destination = average;
    }

    // Using a Collider, check for nearby Redcap Fairies
    public void FindClusterMates()
    {
        ClusterPositions.Clear();

        Collider[] ClusterMates = Physics.OverlapSphere(transform.position, ClusterCheckSize);

        foreach (Collider c in ClusterMates)
        {
            if (c.gameObject.TryGetComponent<RedcapFairyMovement>(out RedcapFairyMovement r)) // theyre name is CLONEenemyredcap etc. etc.
            {
                ClusterPositions.Add(c.gameObject.transform.position);
            }
        }

        //Debug.Log("ClusterPositions Count: " + ClusterPositions.Count);
    }
    // Returns Vector based on average position of all redcaps in Cluster
    public Vector3 GetClusterVector()
    {
        Vector3 average =  new Vector3(0,0,0);
        
        foreach (Vector3 v in ClusterPositions)
        {
            average += v;
        }

        average += transform.position;

        return average/(ClusterPositions.Count+1);
    }

    // Wander State Logic
    public void Wander()
    {
        FindClusterMates();
        Vector3 randomPosition = Random.insideUnitSphere * ClusterWanderRange;
        randomPosition += GetClusterVector();
        Destination = randomPosition;
        //Debug.Log(transform.position);
    }


    // Pursuit State Logic
    public void Pursuit()
    {
        Destination = GameManager.ChosenPlayerCharacter.gameObject.transform.position;
    }

    // State Logic
    public void Update()
    {
        switch (State)
        {
            case RedcapFairyStates.Join:
                Debug.Log("Join");
                if (StartPositions.Count > 0)
                {
                    if (!joining)
                    {
                        Join();
                        joining = true;
                    }

                    Move();


                    if (NavMeshAgent.remainingDistance <= 0)
                    {
                        joining = false;
                        State = RedcapFairyStates.Wander;
                    }
                } else
                {
                    State = RedcapFairyStates.Wander;
                }
                break;

            case RedcapFairyStates.Wander:
                //Debug.Log("Wander");

                if (StateTimer <= 0f)
                {
                    if (!wandering)
                    {
                        Wander();
                        wandering = true;
                    }

                    Move();

                    if (NavMeshAgent.remainingDistance <= 0)
                    {
                        wandering = false;
                        StateTimer = 6f;
                        State = RedcapFairyStates.Idle;
                    }
                } else
                {
                    StateTimer -= Time.deltaTime;
                } 
                break;

            case RedcapFairyStates.Pursuit:
                Pursuit();
                Move();
                break;

            default:
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (State == RedcapFairyStates.Wander)
            {
                Wander(); // maybe there should be a better failsafe for this or change NavMeshAgent.remainingDistance <= 0 to something else
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ClusterCheckSize);
    }
}
