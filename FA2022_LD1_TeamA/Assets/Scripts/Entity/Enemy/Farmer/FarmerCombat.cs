using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmerCombat : EnemyCombat
{
    public FarmerMovement Movement;

    public override void Initialize()
    {
        base.Initialize();
        AttackRange = 10f;
        Movement = GetComponent<FarmerMovement>();
    }

    public override void Attack()
    {
        GameObject bullet = Instantiate(Bullets[0] , gameObject.transform.position, gameObject.transform.rotation);
        
        bullet.GetComponent<BulletCollision>().Owner = gameObject;
        bullet.GetComponent<BulletCollision>().Damage = Damage;
        bullet.GetComponent<BulletCollision>().GetDirection(GetPlayerLocation());
        bullet.GetComponent<BulletCollision>().Lifetime = 3.5f;
        bullet.GetComponent<BulletCollision>().Active = true;

        AttackCooldown = 2f;
    }

    private void Update()
    {
        if (IsPlayerInRange(AttackRange))
        {
            if (Movement.State != FarmerMovement.FarmerStates.Attacking)
            {
                Movement.State = FarmerMovement.FarmerStates.Attacking;
            }

            if (AttackCooldown <= 0)
            {
                Attack();
            }
        } else
        {
            if (Movement.State != FarmerMovement.FarmerStates.Wander)
            {
                Movement.State = FarmerMovement.FarmerStates.Wander;
            }
        }

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }
}
