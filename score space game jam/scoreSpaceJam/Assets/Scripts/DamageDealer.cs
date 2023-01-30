using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private void onTriggerEnter(Collider collision)
    {
        GameObject collisionObj = collision.gameObject;
        if(collisionObj.tag == gameObject.tag) { return; }//can't kill other of the same gameobject

        DamageTaker collisionDmgTakerScript = collisionObj.GetComponent<DamageTaker>();
        if(collisionDmgTakerScript)//if they have dmg taker scrip then they get killed
        {
            collisionDmgTakerScript.killMe();
            Destroy(gameObject);//change this later when polishing
        }
    }
}
