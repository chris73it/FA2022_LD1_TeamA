using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 1;
    public int CurrentHealth = 1;
    public Combat EntityCombat;
    public float HitInvulnerability = 1.5f;
    public GameObject Pickup;
    public bool IsDead = false;

    private void Update() //Debug
    {
        if (Input.GetKeyDown("k") && gameObject.tag == "Player")
        {
            Heal(1);
        }
    }
    public int Heal(int amount)
    {
        if (CurrentHealth + amount >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        } else
        {
            CurrentHealth += amount;
        }

        PlayerUIControl.Instance.AddHeart(amount);

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
            Destroy(gameObject);
            GameManager.Instance.GameState = GameManager.GameStates.GameOver;

        } else if (gameObject.tag == "Enemy")
        {
            // Drop pickup chance
            // if no more enemies in room, set room to is cleared
  
            //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy"));

            if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 1) // The enemy is counted in the array before its destroyed so the length <= 1
            {
                FloorManager.CurrentRoom.IsCleared = true;
            }

            Instantiate(Pickup, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject); 
        }
    }
}
