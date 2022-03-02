using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceTypes resourceType;
    [SerializeField] private ItemWithAmount[] items;

    [SerializeField] private float maxGrowSize = 2;
    [SerializeField] private float growDuration = 40;
    [SerializeField] private float respawnCooldown = 10;

    private float size;
    private float sizePerSec;
    private float respawnTime = 0;
    private bool destroyed = false;

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        size = transform.localScale.x;
        sizePerSec = maxGrowSize / (growDuration * 60);
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
        {
            if (Time.time > respawnTime)
            {
                foreach (Transform lod in transform)
                {
                    lod.gameObject.SetActive(true);
                }
                collider.enabled = true;
                transform.localScale = Vector3.one * 0.1f;
                destroyed = false;
            }
        }
        else
        {
            size = Mathf.Clamp(size + sizePerSec * Time.deltaTime, 0, maxGrowSize);
            transform.localScale = Vector3.one * size;
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
        collider.enabled = true;
        destroyed = true;
    }
}
