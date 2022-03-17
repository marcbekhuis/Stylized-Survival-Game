using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthSystemPlayer : HealthSystem
{
    protected override void OnDeath()
    {
        PhotonNetwork.Destroy(this.gameObject);
        GameManager.instance.ToggleDeathScreen(true);
        EnemySpawner.instance.RemovePlayerTransform(this.transform);
        UseableItem.ClearEvents();
    }
}
