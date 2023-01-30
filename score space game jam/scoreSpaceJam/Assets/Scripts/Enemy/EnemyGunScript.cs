using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    //Components
    [SerializeField] GameObject duplicationProjectile;
    [SerializeField] Transform aimPos;
    PlayerControls playerControls;
    private GameObject player;
    public float coolDown;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(aimPos.position, Vector3.back);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            //if (raycastHit.collider.tag == "Player" && timer < 0 && Random.Range(0f,100f) < 1)
            if (timer < 0 && Random.Range(0f, 100f) < 1)
            {
                Instantiate(duplicationProjectile, aimPos.position, Quaternion.identity, transform.parent.transform.parent);//instantiate bullet at current position with current rotation at top of hierachy
                timer = coolDown;
            }
        }

        timer -= Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "death")
        {
            Destroy(gameObject);
        }
    }
}
