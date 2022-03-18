using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AIMeleeAttack : MonoBehaviourPun
{
    [SerializeField] protected float radius = 5;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float AttackCooldown = 3;
    [SerializeField] protected Transform attackOrigin;
    [SerializeField] protected LayerMask targetLayer;

    public delegate void Attack();
    public event Attack onAttack = delegate { };

    protected float nextAttack = 0;

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
                    CallAttack();
                    photonView.RPC("RPCSyncAttackCooldown", RpcTarget.Others, nextAttack);
                    return;
                }
            }
        }
    }

    [PunRPC]
    protected void RPCSyncAttackCooldown(float nextAttack)
    {
        this.nextAttack = nextAttack;
    }

    protected void CallAttack()
    {
        onAttack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(attackOrigin.position, attackOrigin.position + transform.rotation * Vector3.forward * radius);
    }
}
