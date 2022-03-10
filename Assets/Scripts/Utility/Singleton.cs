using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Singleton<T> : MonoBehaviourPun where T : MonoBehaviourPun
{
    public static T instance;

    protected void Awake()
    {
        if (instance) Destroy(this);
        else instance = this.GetComponent<T>();
    }
}
