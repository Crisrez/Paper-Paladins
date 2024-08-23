using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 2.0f;

    [SerializeField] InputController inputController;
    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputController = InputController.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputController.GetPlayerMovement();

        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        //if (move != Vector3.zero)
        {
            gameObject.transform.forward = cameraTransform.forward;
        }

        // Changes the height position of the player..
        if (inputController.Shoot())
        {
            
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
}
