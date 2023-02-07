using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLogic : MonoBehaviour
{
    // Audio
    public AudioSource SoundSource;
    public List<AudioClip> SoundClips;

    public enum PickupTypes
    {
        Coin,
        Health,
        Stamina
    }
    public Animator Animator;

    public PickupTypes Type;
    public int Value = 0;
    public Price ShopCost;
    public bool Active = false;

    private void Awake()
    {
        if (FloorManager.CurrentRoom.Type == Room.RoomTypes.ShopRoom)
        {
            RandomizeType(1);
        }
        else
        {
            RandomizeType();
        }

        SoundSource = GetComponent<AudioSource>();

        ShopCost.Cost = 0;
        
        Value = Random.Range(1, 3);

        Animator = GetComponentInChildren<Animator>();

        Animator.SetInteger("ItemType", (int)Type);

        Active = true;
    }
    public void RandomizeType()
    {
        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        Type = (PickupTypes)Random.Range(0, pickupTypesLength);
    }

    public void RandomizeType(int start)
    {
        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        Type = (PickupTypes)Random.Range(start, pickupTypesLength);
    }

    public void RandomizeType(int start, int end)
    {
        var pickupTypesLength = PickupTypes.GetNames(typeof(PickupTypes)).Length;
        Type = (PickupTypes)Random.Range(start, end);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Active)
        {
            if (collision.gameObject.tag == "Player" && ShopCost.CanPlayerAfford())
            {
                switch (Type)
                {
                    case PickupTypes.Coin:
                        AudioSource.PlayClipAtPoint(SoundClips[0], transform.position, GameManager.Instance.SoundVolume / 10f);
                        collision.gameObject.GetComponent<Wealth>().Deposit(Value);
                        break;

                    case PickupTypes.Health:
                        AudioSource.PlayClipAtPoint(SoundClips[1], transform.position, GameManager.Instance.SoundVolume / 10f);
                        collision.gameObject.GetComponent<Health>().Heal(Value);
                        break;

                    case PickupTypes.Stamina:
                        AudioSource.PlayClipAtPoint(SoundClips[2], transform.position, GameManager.Instance.SoundVolume / 10f);
                        collision.gameObject.GetComponent<PlayerMovement>().RestoreStamina(1f);
                        break;

                    default:
                        break;
                }

                Debug.Log("You got " + Value + " " + PickupTypes.GetName(typeof(PickupTypes), Type));
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (FloorManager.CurrentRoom.PickupsSpawned.Contains(gameObject))
        {
            FloorManager.CurrentRoom.PickupsSpawned.Remove(gameObject);
        }
    }
}
