using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    //Package Manager Input System:
    //Create Input Actions
    //Action Maps -> Actions -> Properties
    //ezpz.
    //Probably need some sort of acceleration?

    InputMaster controls;
    Vector2 horizontalMovement;
    Vector2 verticalMovementAndRotation;

    private void Awake() {
        controls = new InputMaster();

        controls.Player.HorizontalMovement.performed += ctx => horizontalMovement = ctx.ReadValue<Vector2>();
        controls.Player.HorizontalMovement.canceled += ctx => horizontalMovement = Vector2.zero;

        controls.Player.UpDownYaw.performed += ctx => verticalMovementAndRotation = ctx.ReadValue<Vector2>();
        controls.Player.HorizontalMovement.canceled += ctx => verticalMovementAndRotation = Vector2.zero;

    }

    private void OnEnable() {
        controls.Player.Enable();
    }

    private void OnDisable() {
        controls.Player.Disable();

    }

    private void Update() {

        //Movement is only on a horizontal plane based on right stick input.
        Vector3 hm = new Vector3(horizontalMovement.x, 0, horizontalMovement.y) * Time.deltaTime;
        transform.Translate(hm, Space.Self);

        //UpDown
        Vector3 vm = new Vector3(0, verticalMovementAndRotation.y, 0) * Time.deltaTime;
        transform.Translate(vm, Space.Self);

        //Yaw 
        Vector2 r = new Vector2(0, verticalMovementAndRotation.x) * 100f * Time.deltaTime;
        transform.Rotate(r, Space.Self);
    }
}
