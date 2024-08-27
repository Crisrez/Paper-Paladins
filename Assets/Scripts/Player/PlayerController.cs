using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float playerSpeed;

    [SerializeField] InputController inputController;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject spawnBall;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputController = InputController.Instance;
        cameraTransform = Camera.main.transform;
        playerSpeed = gameObject.GetComponent<PersonajeJugable>().GetVelocidad();
    }

    private void Update()
    {
        Vector2 movement = inputController.GetPlayerMovement();

        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        controller.SimpleMove(move * playerSpeed);

        gameObject.transform.forward = cameraTransform.forward;

        // Changes the height position of the player..
        if (inputController.Shoot())
        {
            gameObject.GetComponent<PersonajeJugable>().Disparar(ballPrefab, spawnBall);
        }

    }
}
