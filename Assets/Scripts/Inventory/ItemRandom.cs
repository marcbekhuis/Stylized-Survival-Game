using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemRandom
{
    public Item item;
    public float chance = 50;
    public int minAmount = 0;
    public int maxAmount = 1;

    public int GetAmount()
    {
        if (Random.Range(0f, 100f) < chance)
            return Random.Range(minAmount, maxAmount);
        return 0;
    }
}
