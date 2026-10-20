using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = (Vector2)cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float angleDeg = (180/Mathf.PI)*angleRad-90;

        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
    }
}
