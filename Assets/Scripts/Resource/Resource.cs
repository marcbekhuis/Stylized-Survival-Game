using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Resource : MonoBehaviourPunCallbacks
{
    [SerializeField] private ResourceTypes resourceType;
    [SerializeField] private ItemWithAmount[] items;

    [SerializeField] private AnimationCurve sizeAge;
    [SerializeField] private float respawnCooldown = 10;

    [SerializeField] private float ageSec;
    private float respawnTime = 0;
    private bool destroyed = false;

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        //if(photonView.IsMine)
        ageSec = GetAgeFromSize();
        collider = GetComponent<Collider>();
    }

    float GetAgeFromSize()
    {
        Keyframe first = sizeAge.keys.First();
        Keyframe last = sizeAge.keys.Last();
        for (int i = 0; i < sizeAge.length; i++)
        {
            if (sizeAge[i].value <= transform.localScale.x)
            {
                first = sizeAge[i];
                if (i == sizeAge.length - 1) return sizeAge[i].time * 60;
                else last = sizeAge[i + 1];
            }
            else
            {
                for (float x = first.time; x < last.time; x+= 0.1f)
                {
                    float scale = sizeAge.Evaluate(x);
                    if (transform.localScale.x > scale - 0.1f && transform.localScale.x < scale + 0.1f)
                        return x * 60;
                }
            }
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            if (GameManager.instance.gameTime > respawnTime)
            {
                foreach (Transform lod in transform)
                {
                    lod.gameObject.SetActive(true);
                }
                collider.enabled = true;
                ageSec = 0;
                transform.localScale = Vector3.one * sizeAge.Evaluate(ageSec / 60f);
                destroyed = false;
            }
        }
        else
        {
            ageSec = Mathf.Clamp(ageSec + Time.deltaTime, 0, sizeAge.keys.Last().time * 60);
            transform.localScale = Vector3.one * sizeAge.Evaluate(ageSec / 60f);
        }
    }

    public bool CanHarvest(ResourceTypes toolResourceType)
    {
        return toolResourceType == resourceType;
    }

    public void Harvest(Inventory[] inventories)
    {
        foreach (var item in items)
        {
            foreach (var inventory in inventories)
            {
                if (inventory.HasSpace())
                {
                    inventory.AddItem(item.item, item.amount);
                    break;
                }
            }
        }
        foreach (Transform lod in transform)
        {
            lod.gameObject.SetActive(false);
        }
        respawnTime = GameManager.instance.gameTime + respawnCooldown;
        collider.enabled = false;
        destroyed = true;
        //photonView.RPC("RPCSyncHarvest", RpcTarget.All);
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    if (!photonView.IsMine) return;
    //    photonView.RPC("RPCSyncPlayerConnect", newPlayer, ageSec, respawnTime, destroyed);
    //}

    //[PunRPC]
    //private void RPCSyncPlayerConnect(float ageSec, float respawnTime, bool destroyed)
    //{
    //    this.ageSec = ageSec;
    //    this.respawnTime = respawnTime;
    //    this.destroyed = destroyed;
    //}

    //[PunRPC]
    //private void RPCSyncHarvest()
    //{
    //    foreach (Transform lod in transform)
    //    {
    //        lod.gameObject.SetActive(false);
    //    }
    //    respawnTime = GameManager.instance.gameTime + respawnCooldown;
    //    collider.enabled = false;
    //    destroyed = true;
    //}
}
