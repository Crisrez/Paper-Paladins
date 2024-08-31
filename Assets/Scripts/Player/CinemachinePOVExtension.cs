using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;

    private InputController inputController;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        inputController = InputController.Instance;
        base.Awake();
    }

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state , float deltaTime)
    {
        if (vcam.Follow && inputController)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }
                Vector2 deltaInput = inputController.GetCamera();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
