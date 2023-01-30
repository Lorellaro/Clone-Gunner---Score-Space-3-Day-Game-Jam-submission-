using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;

    ShakeCamera cameraShakeScript;

    private void Start()
    {
        cameraShakeScript = GameObject.FindGameObjectWithTag("VCam").GetComponent<ShakeCamera>();
    }

    public void killMe()
    {
        if(gameObject.tag == "Player")
        {
            playerDeath();
            return;
        }

        if(deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity, transform.parent);
        }
        if(cameraShakeScript != null)
        {
            cameraShakeScript.shakeCamera();
        }

        Destroy(gameObject);//Change when polishing
    }

    private void playerDeath()//take player life check if lives are 0 yada yada
    {

    }
}
