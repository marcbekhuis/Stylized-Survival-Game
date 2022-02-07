using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Nametags : MonoBehaviourPun
{
    [SerializeField] private Text name;
    [SerializeField] private GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        name.text = photonView.Owner.NickName;
        if (photonView.IsMine)
        {
            Destroy(canvas);
            Destroy(this);
        }
    }

    private void Update()
    {
        Debug.LogError(Camera.current);
        canvas.transform.LookAt(Camera.current.transform);
        canvas.transform.Rotate(new Vector3(180, 0, 0));
    }
}
