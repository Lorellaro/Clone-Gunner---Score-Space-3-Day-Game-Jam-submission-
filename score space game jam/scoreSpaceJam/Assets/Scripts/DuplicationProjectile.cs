using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicationProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float deathTime;

    public static bool hit = false;
    Rigidbody rb;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Vector3 direction = transform.forward;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = transform.forward;
        rb.MovePosition(transform.position + (direction * projectileSpeed * Time.deltaTime));

        if (deathTime <= 0 || hit)
        {
            //Die();
        }
        else
        {
            deathTime -= Time.deltaTime;
        }
    }
    void Die()
    {
        // play destroyed sound effect
        // destroy bullet
        hit = false;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Die();

        if (LayerMask.LayerToName(other.gameObject.layer) == "Wall")
        {
            Die();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "death")
        {
            Destroy(gameObject);
        }
    }
}
