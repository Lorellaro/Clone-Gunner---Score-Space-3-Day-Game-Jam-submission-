using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duplicationEnemyMovement : MonoBehaviour
{
    //Components
    private Rigidbody rb;
    private GameObject player;

    //Variables
    public float movementSpeed = 100f;
    private Vector3 directionVector;
    public float offsetDistance;

    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Calculate Target Vector

        //Calculate offsets for circle movement
        float xOffset = Mathf.Sin(Time.frameCount * 0.01f) * offsetDistance;
        float zOffset = Mathf.Cos(Time.frameCount * 0.01f) * offsetDistance;

        //Direction towards the middle of the screen in height and the player position in x 
        directionVector = new Vector3(player.transform.position.x - transform.position.x,0f,0f-transform.position.z);
        //Add Circle motion
        directionVector.x += xOffset;
        directionVector.z += zOffset;
        //move the enemy
        rb.velocity = directionVector.normalized * movementSpeed * Time.deltaTime;
    }
}
