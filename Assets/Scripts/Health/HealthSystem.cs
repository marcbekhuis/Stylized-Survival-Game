using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthSystem : MonoBehaviourPun
{
    public delegate void OnHealthChange(float health, float maxHealth);
    public event OnHealthChange onHealthChanged = delegate { };
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public float Health { get=>health; set {
            health = Mathf.Clamp(value, 0, maxHealth);
            onHealthChanged(health, maxHealth);
            photonView.RPC("RPCSyncHealth", RpcTarget.Others, health, maxHealth);
            if(health <= 0)
            {
                OnDeath();
            }
        } 
    }

    protected virtual void OnDeath()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }

    [PunRPC]
    private void RPCSyncHealth(float health, float maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        onHealthChanged(this.health, this.maxHealth);
    }

    private void Awake()
    {
        Health = maxHealth;
    }
}