using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    public float AttackRange = -1f;
    public static GameObject Player;
    public List<GameObject> Bullets;

    private void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (Player == null)
        {
            Player = GameManager.ChosenPlayerCharacter;
        }
    }
    public bool IsPlayerInRange(float range)
    {
        if (Player != null)
        {
            return Vector3.Distance(transform.position, Player.transform.position) <= range;
        }

        Debug.Log("Player is null");
        return false;
    }

    public Vector3 GetPlayerLocation()
    {
        if (Player != null)
        {
            return Player.transform.position;
        }

        Debug.Log("Player is null");
        return new Vector3(-10f, -10f, -10f);
    }
}
