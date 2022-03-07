using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField] private Image fill;

    public void UpdateBar(float value, float maxValue)
    {
        fill.fillAmount = value / maxValue;
    }
}
