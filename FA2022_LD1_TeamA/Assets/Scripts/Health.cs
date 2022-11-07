using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 1;
    public int CurrentHealth;
    public Combat EntityCombat;
    public float HitInvulnerability = 1.5f;
    public GameObject Pickup;
    public bool IsDead = false;

    public bool IsDoT = false;
    public Combat.DamageOverTime DoT;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    private void Update() 
    {
        if (Input.GetKeyDown("k") && gameObject.tag == "Player") //Debug
        { 
            Die();
        }

        if (IsDoT)
        {
            if (DoT.TotalDuration > 0)
            {
                DoT.Timer += Time.deltaTime;

                if (DoT.Timer >= DoT.TimeToDamage)
                {
                    TakeDamage(DoT.Damage);
                    DoT.TotalDuration -= DoT.Timer;
                    DoT.Timer = 0f;
                }
            } else
            {
                IsDoT = false;
            }
        }
    }

    public int Heal(int amount)
    {
        if (CurrentHealth + amount >= MaxHealth)
        {
            PlayerUIControl.Instance.AddHeart(MaxHealth - CurrentHealth);
            CurrentHealth = MaxHealth;
        }
        else
        {
            PlayerUIControl.Instance.AddHeart(amount);
            CurrentHealth += amount;
        }

        return CurrentHealth;
    }
    public int TakeDamage(int amount)
    {
        if (EntityCombat.Invulnerability <= 0 || IsDead) // Obstacle doesn't need a Combat
        {
            CurrentHealth -= amount;

            if (gameObject.tag == "Player")
            {
                EntityCombat.SetGeneralInvulnerability(HitInvulnerability);
                PlayerUIControl.Instance.RemoveHeart(amount);
            }

            if (CurrentHealth <= 0)
            {
                Die();
            }

            Debug.Log(gameObject.tag + " Health:" + CurrentHealth);
        }
        return CurrentHealth;
    }

    public void Die()
    {
        IsDead = true;
        if (gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.Instance.GameState = GameManager.GameStates.GameOver;

        }
        else if (gameObject.tag == "Enemy")
        {
            // Drop pickup chance
            // if no more enemies in room, set room to is cleared

            //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy"));

            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1) // The enemy is counted in the array before its destroyed so the length <= 1
            {
                FloorManager.CurrentRoom.IsCleared = true;
            }

            if (Random.Range(0f, 1f) >= 0.75f)
            {
                Instantiate(Pickup, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        } else
        {
            if (Random.Range(0f, 1f) >= 0.25f)
            {
                Instantiate(Pickup, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
