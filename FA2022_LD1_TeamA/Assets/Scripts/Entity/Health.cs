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

    /// Obstacle
    // Position of appropriate Prefab in List
    public int ObstacleIndex;

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
        //Debug.Log("1");

        if (EntityCombat.Invulnerability <= 0 || IsDead) // Obstacle doesn't need a Combat
        {
            //Debug.Log("2");

            CurrentHealth -= amount;


            //Debug.Log("3");

            if (gameObject.tag == "Player")
            {
                //Debug.Log("3a");

                SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);

                //Debug.Log("3b");

                EntityCombat.SetGeneralInvulnerability(HitInvulnerability);

                //Debug.Log("3c");

                PlayerUIControl.Instance.RemoveHeart(amount);
            }

            //Debug.Log("4");

            if (gameObject.tag == "Enemy")
            {
                Debug.Log("4a");

                SoundSource.PlayOneShot(SoundClips[0], GameManager.Instance.SoundVolume / 10f);
            }

            //Debug.Log("5");

            EntityCombat.OnDamageAnimation();

            //Debug.Log("6");

            EntityCombat.InvulernabilityAnimation();


            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Die();
            }

            Debug.Log(gameObject.tag + " Health:" + CurrentHealth);
        }
        return CurrentHealth;
    }

    public void Die()
    {
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

            /*
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy"));
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

            if (Random.Range(0f, 1f) >= 0.75f)
            {
                Instantiate(Pickup, gameObject.transform.position, gameObject.transform.rotation);
            }

            Destroy(gameObject);
        }
        else
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
            if (FloorManager.CurrentRoom.EnemiesSpawned.Contains(gameObject)) // O(a) maybe we can just get rid of the check and base it off HasEntered
            {
                FloorManager.CurrentRoom.EnemiesSpawned.Remove(gameObject);
            }

            if (FloorManager.CurrentRoom.EnemiesSpawned.Count <= 0) // The enemy is counted in the array before its destroyed so the length <= 1, deprecated since enemy count is cleared
            {
                FloorManager.CurrentRoom.IsCleared = true;
            }
        }
        else if (gameObject.tag == "Obstacle")
        {
            if (!IsDead)
            {
                Debug.Log("Obstacle Added");
                FloorManager.PreviousRoom.Manager.ObstaclesSpawned.Add((ObstacleIndex, this.gameObject.transform.position, this.gameObject.transform.rotation));
            }
        }


    }
}
