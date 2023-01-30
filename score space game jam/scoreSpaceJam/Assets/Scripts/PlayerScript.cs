using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class PlayerScript : MonoBehaviour
{
    //Components
    private PlayerControls playerControls;
    private Rigidbody rb;
    public GameObject pauseMenuContainer;
    [SerializeField] GameObject playerExplosionVFX;
    public AudioSource audioSource;
    public AudioClip explosion;

    //Variables
    private Vector2 inputVector;
    public float movementSpeed = 20f;
    public static int lives = 3;

    // blink when hit
    Renderer pRender;
    public Renderer gun;

    ShakeCamera shakeCamereScript;

    public float blink;
    float blinkTime = 0.1f;
    float invincibleTime;
    public float invincibleTotalTime = 5f;
    bool invincible = false;


    void Awake()
    {
        //Get Components
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        rb = GetComponent<Rigidbody>();
        pRender = GetComponentInChildren<MeshRenderer>();
        lives = 3;
        shakeCamereScript = GameObject.FindGameObjectWithTag("VCam").GetComponent<ShakeCamera>();
    }

    private void Update()
    {
        if (invincibleTime > 0)
        {
            invincible = true;
            invincibleTime -= Time.deltaTime;
            blinkTime -= Time.deltaTime;

            if (blinkTime <= 0)
            {
                pRender.enabled = !pRender.enabled;
                gun.enabled = !pRender.enabled;
            }

            if (invincibleTime <= 0)
            {
                pRender.enabled = true;
                gun.enabled = true;
                invincible = false;
            }
        }
        if (playerControls.Player.Pause.triggered)
        {
            pauseMenuContainer.SetActive(!pauseMenuContainer.activeSelf);
            Time.timeScale = pauseMenuContainer.activeSelf ? 0 : 1;
        }
    }

    private void FixedUpdate()
    {
        Vector3 input = playerControls.Player.Movement.ReadValue<Vector2>();

        input.z = 0;
        input.y = 0;

        rb.velocity = input * movementSpeed * Time.deltaTime;
    }

    // collision with enemy / enemy bullet
    private void OnCollisionEnter(Collision other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enemy" || LayerMask.LayerToName(other.gameObject.layer) == "EnemyBullets")
        {
            if (!invincible)
            {
                // iframes 
                // animate so it disappears then comes back 
                audioSource.PlayOneShot(explosion, 0.5f);
                Instantiate(playerExplosionVFX, transform.position, Quaternion.identity, transform.parent);
                shakeCamereScript.shakeCamera();
                invincibleTime = invincibleTotalTime;
                lives--;
            }

        }
    }
}
