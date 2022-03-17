using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AIMeleeAttack : MonoBehaviourPun
{
    [SerializeField] private float radius = 5;
    [SerializeField] private float damage = 5;
    [SerializeField] private float AttackCooldown = 3;
    [SerializeField] private Transform attackOrigin;
    [SerializeField] private LayerMask targetLayer;

    public delegate void Attack();
    public event Attack onAttack = delegate { };

    private float nextAttack = 0;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameTime > nextAttack)
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(attackOrigin.position, this.transform.rotation * Vector3.forward, radius, targetLayer);

            foreach (var item in raycastHits)
            {
                if (item.transform.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
                {
                    healthSystem.Health -= damage;
                    nextAttack = GameManager.instance.gameTime + AttackCooldown;
                    onAttack();
                    photonView.RPC("RPCSyncAttackCooldown", RpcTarget.Others, nextAttack);
                    return;
                }
            }
        }
    }

    [PunRPC]
    private void RPCSyncAttackCooldown(float nextAttack)
    {
        this.nextAttack = nextAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(attackOrigin.position, attackOrigin.position + transform.rotation * Vector3.forward * radius);
    }
}
