using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController: MonoBehaviour
{
    [SerializeField] PlayerActions playerActions;

    private static InputController instance;

    public static InputController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerActions.Gameplay.Move.ReadValue<Vector2>();
    }

    public Vector2 GetCamera()
    {
        return playerActions.Gameplay.Look.ReadValue<Vector2>();
    }

    public bool Shoot()
    {
        return playerActions.Gameplay.Attack.triggered;
    }


} 
