using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Nametags : MonoBehaviourPun
{
    [SerializeField] private Text name;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform camera;


    // Start is called before the first frame update
    void Start()
    {
        name.text = photonView.Owner.NickName;
        if (photonView.IsMine)
        {
            Destroy(canvas);
            Destroy(this);
        }
        else
        {
            camera = GameObject.FindObjectOfType<Camera>().transform;
        }
    }

    private void Update()
    {
        canvas.transform.LookAt(camera);
        canvas.transform.Rotate(new Vector3(0, 180, 0));
    }
}
