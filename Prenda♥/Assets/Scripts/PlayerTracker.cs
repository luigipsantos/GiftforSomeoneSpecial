using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public float mouseSensitivity = 10.0f;
    public Transform player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        Vector3 rotPlayer = player.transform.rotation.eulerAngles;

        rotPlayer.y += rotAmountX;
        rotPlayer.x += -rotAmountY;

        player.rotation = Quaternion.Euler(rotPlayer);
    }
}
