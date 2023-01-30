using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    SpawnerScript spawnerScript;
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    public GameObject enemyPrefab;
    Rigidbody rb;

    private void Awake()
    {
        spawnerScript = FindObjectOfType<SpawnerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waveConfig = spawnerScript.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        rb = GetComponent<Rigidbody>();
        rb.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            //rb.velocity += (Vector3.MoveTowards(transform.position, targetPosition, delta));
            //rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, delta));
            rb.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
            if (rb.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // collides with bullet duplicate itself 
        if (LayerMask.LayerToName(collision.gameObject.layer) == "PlayerBullet")
        {
            // send destroy to bullet 
            DuplicationProjectile.hit = true;

            // create new
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}
