using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathWater : MonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private Vector3 halfSize = Vector3.one;
    [SerializeField] private LayerMask layers;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(this.transform.position + offset, halfSize, new Quaternion(0,0,0,0), layers);

        foreach (var collider in colliders)
        {
            if (!collider.transform.GetComponent<PhotonView>().IsMine) return;
            else
            {
                collider.transform.GetComponent<HealthSystem>().Health -= (this.transform.position.y - collider.transform.position.y) * Time.deltaTime;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,0,1,0.5f);
        Gizmos.DrawCube(this.transform.position + offset, halfSize * 2);
    }
}
