using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AIMeleeAttackSphere : AIMeleeAttack
{
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameTime > nextAttack)
        {
            Collider[] colliders = Physics.OverlapSphere(attackOrigin.position, radius, targetLayer);

            foreach (var item in colliders)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(attackOrigin.position, radius);
    }
}
