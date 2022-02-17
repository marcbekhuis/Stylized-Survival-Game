using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void OnHealthChange(float health, float maxHealth);
    public event OnHealthChange onHealthChanged = delegate { };
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public float Health { get=>health; set {
            health = value;
            onHealthChanged(health, maxHealth);
        } 
    }
}