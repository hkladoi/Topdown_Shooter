using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeDestroyer : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

}
