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
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ItemRandom[] deathItemDrops;

    private Inventory[] lastDamagingInventories;

    public float Health
    {
        get => health; set
        {
            if (hitParticle)
                if(value < health)
                    hitParticle.Play();
            health = Mathf.Clamp(value, 0, maxHealth);
            onHealthChanged(health, maxHealth);
            if (photonView.IsMine)
                photonView.RPC("RPCSyncHealth", RpcTarget.Others, health, maxHealth);
            if (health <= 0)
            {
                OnDeath();
            }
        }
    }

    public void TakeDamage(float damage, Inventory[] inventory)
    {
        lastDamagingInventories = inventory;
        health -= damage;
    }

    protected virtual void OnDeath()
    {
        if (lastDamagingInventories != null)
        {
            foreach (var item in deathItemDrops)
            {
                foreach (var inventory in lastDamagingInventories)
                {
                    if (inventory.HasSpace() || inventory.HasItemSlotWithSpace(item.item))
                    {
                        int amount = item.GetAmount();
                        if (amount > 0)
                            inventory.AddItem(item.item, amount);
                        break;
                    }
                }
            }
        }

        PhotonNetwork.Destroy(this.gameObject);
    }

    [PunRPC]
    protected void RPCSyncHealth(float health, float maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        if (hitParticle)
            hitParticle.Play();
        onHealthChanged(this.health, this.maxHealth);
    }

    private void Awake()
    {
        Health = maxHealth;
    }
}