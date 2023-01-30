using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DuplicationGun : MonoBehaviour
{
    //[SerializeField] float 
    [SerializeField] GameObject duplicationProjectile;
    [SerializeField] Transform aimPos;
    [SerializeField] float timeBtwShots = 0.5f;
    public AudioSource audiosource;
    public AudioClip shootClip;

    PlayerControls playerControls;

    bool LMBisPressed;

    float currentTimeBtwShots = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerControls.Player.ShootTap.triggered)
        {
            fireBullets();
        }
        playerControls.Player.Shoot.performed += _ => LMBisPressed = true;
        playerControls.Player.Shoot.canceled += _ => LMBisPressed = false;

        if(LMBisPressed)//mouse is held down
        {
            fireBullets();
        }

        currentTimeBtwShots -= Time.deltaTime;

    }

    private void fireBullets()
    {
        if(currentTimeBtwShots < 0)
        {
            audiosource.PlayOneShot(shootClip,1);
            Instantiate(duplicationProjectile, aimPos.position, Quaternion.identity, transform.parent.transform.parent);//instantiate bullet at current position with current rotation at top of hierachy
            currentTimeBtwShots = timeBtwShots;
        }
    }
}
