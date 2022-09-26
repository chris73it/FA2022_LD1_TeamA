using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 1;
    public int CurrentHealth = 1;
    public Combat EntityCombat;
    public float HitInvulnerability = 1.5f;

    private void Start()
    {
        Debug.Log(EntityCombat);
    }
    public int Heal(int amount)
    {
        CurrentHealth += amount;
        return CurrentHealth;
    }
    public int TakeDamage(int amount)
    {
        if (EntityCombat.Invulnerability <= 0)
        {
            CurrentHealth -= amount;
            EntityCombat.SetGeneralInvulnerability(HitInvulnerability);
            // Check Death
            Debug.Log("Health:" + CurrentHealth);
        }
        return CurrentHealth;
    } 
}
