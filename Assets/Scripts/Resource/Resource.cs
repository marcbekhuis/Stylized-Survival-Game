using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Resource : MonoBehaviourPunCallbacks
{
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private ResourceTypes resourceType;
    [SerializeField] private ItemRandom[] items;

    [SerializeField] private AnimationCurve sizeAge;
    [SerializeField] private float respawnCooldown = 10;
    [SerializeField] private float animationDuration = 2;
    [SerializeField] private ParticleSystem harvestEffect;

    [SerializeField] private float ageSec;
    private float respawnTime = 0;
    private bool destroyed = false;
    private float health;
    private Quaternion normalRotation;

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        //if(photonView.IsMine)
        health = maxHealth;
        ageSec = GetAgeFromSize();
        collider = GetComponent<Collider>();
        normalRotation = transform.rotation;
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
                transform.rotation = normalRotation;
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

    public void Harvest(Inventory[] inventories, float damage, Transform harvesterTransform)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        harvestEffect.Play();
        if (health > 0) return;

        foreach (var item in items)
        {
            foreach (var inventory in inventories)
            {
                if (inventory.HasSpace() || inventory.HasItemSlotWithSpace(item.item))
                {
                    int amount = item.GetAmount();
                    if(amount > 0)
                        inventory.AddItem(item.item, amount);
                    break;
                }
            }
        }

        StartCoroutine(Animation(harvesterTransform));
        respawnTime = GameManager.instance.gameTime + respawnCooldown;
        destroyed = true;
        //photonView.RPC("RPCSyncHarvest", RpcTarget.All);
    }

    private IEnumerator Animation(Transform harvesterTransform)
    {
        Vector2 direction = new Vector2(this.transform.position.x - harvesterTransform.position.x, this.transform.position.z - harvesterTransform.position.z).normalized;
        collider.enabled = false;
        for (float i = 0; i < animationDuration; i += Time.deltaTime)
        {
            float progress = i / animationDuration;
            this.transform.Rotate(new Vector3(90 * direction.x * Time.deltaTime / animationDuration, 0, 90 * direction.y * Time.deltaTime / animationDuration),Space.World);
            yield return null;
            
        }
        foreach (Transform lod in transform)
        {
            lod.gameObject.SetActive(false);
        }
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
