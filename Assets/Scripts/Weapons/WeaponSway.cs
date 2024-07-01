using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSway : MonoBehaviour
{
    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        float aimX = inputManager.aimInput.x * multiplier;
        float aimY = inputManager.aimInput.y * multiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-aimY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(aimX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }

    [Header("Sway")]
    [SerializeField] private float smooth;
    [SerializeField] private float multiplier;

    private InputManager inputManager;
}
