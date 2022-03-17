using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FoodSystem : MonoBehaviourPun
{
    public delegate void OnFoodChange(float food, float maxFood);
    public event OnFoodChange onFoodChanged = delegate { };
    [SerializeField] private float food;
    [SerializeField] private float maxFood;
    [SerializeField] private float healAbove;
    [SerializeField] private float damagePerSec = 1;
    [SerializeField] private float healingPerSec = 1;
    [SerializeField] private float idleFoodConsumption = 0.1f;
    [SerializeField] private float walkFoodConsumption = 1f;
    [SerializeField] private float runFoodConsumption = 2f;

    private HealthSystem healthSystem;
    private CharacterMovement characterMovement;


    private void Start()
    {
        this.TryGetComponent<HealthSystem>(out healthSystem);
        this.TryGetComponent<CharacterMovement>(out characterMovement);
        Food = maxFood;
    }

    public void Update()
    {
        if (healthSystem)
        {
            if(food <= 0)
            {
                healthSystem.Health -= damagePerSec * Time.deltaTime;
            }
            if(food >= healAbove)
            {
                healthSystem.Health += healingPerSec * Time.deltaTime;
            }
        }
        if (characterMovement)
        {
            switch (characterMovement.GetMovement)
            {
                case CharacterMovement.MovementType.Idle:
                    Food -= idleFoodConsumption * Time.deltaTime;
                    break;
                case CharacterMovement.MovementType.Walk:
                    Food -= walkFoodConsumption * Time.deltaTime;
                    break;
                case CharacterMovement.MovementType.Run:
                    Food -= runFoodConsumption * Time.deltaTime;
                    break;
                default:
                    break;
            }
        }
    }

    public float Food
    {
        get => food; set
        {
            food = Mathf.Clamp(value, 0, maxFood);
            onFoodChanged(food, maxFood);
            photonView.RPC("RPCSyncFood", RpcTarget.Others, food, maxFood);
        }
    }

    [PunRPC]
    private void RPCSyncFood(float food, float maxFood)
    {
        this.food = food;
        this.maxFood = maxFood;
        onFoodChanged(this.food, this.maxFood);
    }
}
