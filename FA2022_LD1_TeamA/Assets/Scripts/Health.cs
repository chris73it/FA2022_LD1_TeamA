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

    // Audio
    public AudioSource SoundSource;
    public List<AudioClip> SoundClips;

    private void Awake()
    {
        SoundSource = GetComponent<AudioSource>();
        CurrentHealth = MaxHealth;
    }
    private void Update() 
    {
        /*
        if (Input.GetKeyDown("k") && gameObject.tag == "Player") //Debug
        { 
            Die();
        }
        */

        if (!IsDead)
        {
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
                }
                else
                {
                    IsDoT = false;
                }
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
                SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);
                EntityCombat.SetGeneralInvulnerability(HitInvulnerability);
                PlayerUIControl.Instance.RemoveHeart(amount);
            }

            if (gameObject.tag == "Enemy")
            {
                SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);
            }

            EntityCombat.OnDamageAnimation();
            EntityCombat.InvulernabilityAnimation();


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
            AudioSource.PlayClipAtPoint(SoundClips[1], transform.position, GameManager.Instance.SoundVolume / 10f);
            Destroy(gameObject);
            GameManager.Instance.GameState = GameManager.GameStates.GameOver;
        }
        else if (gameObject.tag == "Enemy")
        {
            // Drop pickup chance
            // if no more enemies in room, set room to is cleared

            /*ebug.Log(GameObject.FindGameObjectsWithTag("Enemy"));
            for (int i = 0; i < FloorManager.CurrentRoom.EnemiesSpawned.Count; i++) 
            {
                if (gameObject == FloorManager.CurrentRoom.EnemiesSpawned[i])
                {
                    FloorManager.CurrentRoom.EnemiesSpawned.Remove(gameObject); // onyl needs this
                    Debug.Log("Enemies Spawned:" + FloorManager.CurrentRoom.EnemiesSpawned.Count);

                    break;
                }
            }
            */

            if (FloorManager.CurrentRoom.EnemiesSpawned.Count <= 0) // The enemy is counted in the array before its destroyed so the length <= 1, deprecated since enemy count is cleared
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
            AudioSource.PlayClipAtPoint(SoundClips[0], transform.position, GameManager.Instance.SoundVolume / 10f);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.tag == "Enemy")
        {
            if (FloorManager.CurrentRoom.EnemiesSpawned.Contains(gameObject))
            {
                FloorManager.CurrentRoom.EnemiesSpawned.Remove(gameObject);
            }
        } else if (gameObject.tag == "Obstacle")
        {
            if (FloorManager.CurrentRoom.ObstaclesSpawned.Contains(gameObject))
            {
                FloorManager.CurrentRoom.ObstaclesSpawned.Remove(gameObject);
            }
        }

        
    }
}
