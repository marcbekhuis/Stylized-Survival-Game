using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemWithAmount
{
    public Item item;
    [SerializeField] public int amount = 1;
}
