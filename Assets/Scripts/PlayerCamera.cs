using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviourPun
{
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float minAngle = -40;
    [SerializeField] private float maxAngle = 80;
    public static PlayerCamera instance;

    private float currentAngle = 0;

    private void Awake()
    {
        if (!photonView.IsMine) Destroy(this.gameObject);
        else instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (GameManager.instance.playerGameInputBlocked) return;
        currentAngle = Mathf.Clamp(currentAngle + Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime, minAngle, maxAngle);
        transform.localEulerAngles = new Vector3(currentAngle, 0, 0);
    }
}
