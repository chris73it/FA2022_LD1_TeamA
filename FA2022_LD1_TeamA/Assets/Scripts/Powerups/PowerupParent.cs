using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParent : MonoBehaviour
{
    public Price ShopCost;
    public string ItemName;
    public string ItemDescription;

    // Audio
    public AudioSource SoundSource;
    public List<AudioClip> SoundClips;

    private void Awake()
    {
        SoundSource = GetComponent<AudioSource>();

        Instantiate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && ShopCost.CanPlayerAfford())
        {
            Debug.Log("PowerupParent onTriggerEnter");
            FloorManager.CurrentRoom.Reward = null;
            ActivatePowerup(collision);
            GameObject g = Instantiate(GameManager.Instance.PowerupUIPrefab);
            g.GetComponent<PowerupUIControl>().Activate(ItemName, ItemDescription);
            AudioSource.PlayClipAtPoint(SoundClips[0], transform.position, GameManager.Instance.SoundVolume / 10f);
            Destroy(gameObject);
        }   
    }

    public virtual void ActivatePowerup(Collision collision)
    {
        Debug.Log("Using PowerupParent");
    }

    public virtual void Instantiate()
    {
        ShopCost.Cost = 0;
    }
}
