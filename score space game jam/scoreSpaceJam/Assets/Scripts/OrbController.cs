using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OrbController : MonoBehaviour
{
    [SerializeField] GameObject particleVFX;
    public GameObject orbPrefab;
    Rigidbody rb;
    public AudioSource audiosource;
    public AudioClip explosion;
    public AudioClip bounce;
    public GameObject orbVisual;

    public float moveSpeed = 100f;

    Vector3 orbDirection;
    public int max = 30;
    public GameObject orbsContainer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Random.rotation;
        orbDirection = transform.forward;

    }


    // move orb
    private void FixedUpdate()
    {
        Move();

        //applies rotation to orb just to make it look cooler
        rb.AddTorque(Vector3.right * 500f * Time.deltaTime);
    }


    void Move()
    {
        rb.velocity = orbDirection * Time.deltaTime * moveSpeed;//Used to be a +=
        //rb.MovePosition(transform.position + (orbDirection * Time.deltaTime * moveSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // collides with bullet duplicate itself 
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerBullet")
        {
            if (moveSpeed == 0)
            {
                moveSpeed = 15;
            }
            // give points
            GameController.score += 100;
            // send destroy to bullet 
            DuplicationProjectile.hit = true;

            // create new
            if (orbsContainer.transform.childCount < max)
            {
                Instantiate(orbPrefab, transform.position, transform.rotation, transform.parent);
            }
        }

        // collide with enemy destroy itself and enemy
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            audiosource.PlayOneShot(explosion, 0.5f);
            // give points 
            GameController.score += 1000;
            // this code here laurent
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            // this code here 
            collision.gameObject.GetComponent<DamageTaker>().killMe();//Kill enemy
            Instantiate(particleVFX, transform.position, Quaternion.identity, transform.parent);
            orbVisual.SetActive(false);
            // 0.5f for after effect has played
            StartCoroutine(destroyOrb());
        }
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Wall" || LayerMask.LayerToName(collision.gameObject.layer) == "Orb")
        {
            audiosource.PlayOneShot(bounce, 0.2f);
            // reflects off wall and other orbs
            orbDirection = Vector3.Reflect(orbDirection, collision.contacts[0].normal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "death")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator destroyOrb()
    {
        yield return new WaitForSeconds(explosion.length);
        Destroy(gameObject, 0.5f);
    }
}
