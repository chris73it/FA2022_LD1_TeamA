using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 1;
    public int CurrentHealth = 1;
    public Combat EntityCombat;
    public float HitInvulnerability = 1.5f;
    public bool IsDead = false;
    public int Heal(int amount)
    {
        CurrentHealth += amount;
        return CurrentHealth;
    }
    public int TakeDamage(int amount)
    {
        if (EntityCombat.Invulnerability <= 0 || IsDead)
        {
            CurrentHealth -= amount;
            if (gameObject.tag == "Player")
            {
                EntityCombat.SetGeneralInvulnerability(HitInvulnerability);
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
            // Game over
        } else if (gameObject.tag == "Enemy")
        {
            // Drop pickup chance
            // if no more enemies in room, set room to is cleared
            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                FloorManager.CurrentRoom.IsCleared = true;
            }
            Destroy(gameObject);
        }
    }
}
