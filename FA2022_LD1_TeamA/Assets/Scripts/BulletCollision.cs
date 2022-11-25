using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject Owner;
    public float Lifetime; // maybe should based on distance the bullet went instead
    public Vector3 Direction;
    public int Damage;
    public bool Active = false;
    public float Speed;
    public string OwnerTag;

    private void Awake()
    {
        OwnerTag = Owner.tag;
    }
    private void Update()
    {
        if (Active)
        {
            if (Lifetime > 0)
            {
                Lifetime -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
            gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.transform.position + (Direction * Time.deltaTime * Speed));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Active)
        {
            if ((other.CompareTag("Enemy") || other.CompareTag("Player")) && !other.CompareTag(OwnerTag))
            {
                other.gameObject.GetComponent<Health>().TakeDamage(Damage);
                Destroy(this.gameObject);
            }
        }
    }

    public void GetDirection(Vector3 position)
    {
        Vector3 heading = position - gameObject.transform.position;
        Direction = heading / (heading.magnitude);
    }

    private void OnDestroy()
    {
        if (Owner != null)
        {
            Owner.GetComponent<Combat>().BulletsCreated.Remove(this.gameObject);
        }
    }
}
