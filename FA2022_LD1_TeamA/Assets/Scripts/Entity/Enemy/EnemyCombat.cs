using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    public float RetreatRange = -1f;
    public float AttackRange = -1f;
    public float SightRange = -1f;
    public static GameObject Player;
    

    private void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        SoundSource = GetComponent<AudioSource>();

        if (Player == null)
        {
            Player = GameManager.ChosenPlayerCharacter;
        }

        AnimationInitialization();
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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
        }
    }

    public GameObject CreateBullet(int damage, float lifeTime = 3.5f, bool rotation = true)
    {
        GameObject bullet = Instantiate(Bullets[0], gameObject.transform.position, gameObject.transform.rotation);

        BulletsCreated.Add(bullet);

        bullet.GetComponent<BulletCollision>().Owner = gameObject;
        bullet.GetComponent<BulletCollision>().OwnerTag = gameObject.tag;
        bullet.GetComponent<BulletCollision>().Damage = damage;

        if (rotation)
        {
            bullet.GetComponent<BulletCollision>().GetDirection(GetPlayerLocation());
        }

        bullet.GetComponent<BulletCollision>().Lifetime = lifeTime;
        bullet.GetComponent<BulletCollision>().Active = true;

        return bullet;
    }

    public Vector3 GetEnemyDirection()
    {
        Vector3 playerDistance;
        if (GameManager.ChosenPlayerCharacter != null)
        {
            playerDistance = (Player.transform.position - transform.position);
            float radius = Mathf.Sqrt(Mathf.Pow(playerDistance.z, 2) + Mathf.Pow(playerDistance.x, 2));
            playerDistance.z = playerDistance.z / radius * 2;
            playerDistance.x = playerDistance.x / radius * 2;
            return playerDistance;
        }

        return new Vector3(-10, -10, -10);
    }

    public override void OnDamage(Collider[] Damaged)
    {
        if (Damaged.Length > 0)
        {
            for (int i = 0; i < Damaged.Length; i++)
            {
                if (Damaged[i].gameObject.tag == "Player")
                {
                    Damaged[i].gameObject.GetComponent<Health>().TakeDamage(Damage);
                    Debug.Log("Hit");
                }
            }
        }
    }
}
