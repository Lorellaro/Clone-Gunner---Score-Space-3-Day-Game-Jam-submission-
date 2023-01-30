using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField] float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToWait);
    }
}
