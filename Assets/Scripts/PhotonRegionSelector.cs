using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonRegionSelector : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Region[] regions = new Region[] { 
    new Region("Asia", "asia"),
    new Region("Australia", "au"),
    new Region("Canada, East", "cae"),
    new Region("Europe", "eu"),
    new Region("India", "in"),
    new Region("Japan", "jp"),
    new Region("Russia", "ru"),
    new Region("Russia, East", "rue"),
    new Region("South Africa", "za"),
    new Region("South America", "sa"),
    new Region("South Korea", "kr"),
    new Region("Turkey", "tr"),
    new Region("USA, East", "us"),
    new Region("USA, West", "usw"),
    };

    [System.Serializable]
    public class Region 
    {
        public string display;
        public string value;

        public Region(string display, string value)
        {
            this.display = display;
            this.value = value;
        }
    }


    private void Awake()
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (var region in regions)
        {
            options.Add(new Dropdown.OptionData(region.display));
        }

        dropdown.options = options;
        dropdown.value = GetRegionIndex(PhotonNetwork.CloudRegion);
        dropdown.onValueChanged.AddListener(SetRegion);
    }

    public void SetRegion(int index)
    {
        PhotonNetwork.ConnectToRegion(regions[index].value);
    }

    private int GetRegionIndex(string value)
    {
        for (int i = 0; i < regions.Length; i++)
        {
            if (regions[i].value == value) return i;
        }
        return default;
    }
}
