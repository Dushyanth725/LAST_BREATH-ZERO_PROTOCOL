using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2.75f;
    public float gravity = -9.81f;

    [Header("Crouch")]
    public Transform playerCamera;
    public float normalCameraHeight = 1.6f;
    public float crouchCameraHeight = 0.95f;
    public float cameraTransitionSpeed = 6f;

    private CharacterController controller;
    private Vector3 velocity;

    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //==========================================
        // CROUCH TOGGLE
        //==========================================

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
        }

        //==========================================
        // CAMERA CROUCH
        //==========================================

        float targetCameraHeight = isCrouching
            ? crouchCameraHeight
            : normalCameraHeight;

        Vector3 cameraPosition = playerCamera.localPosition;

        cameraPosition.y = Mathf.Lerp(
            cameraPosition.y,
            targetCameraHeight,
            cameraTransitionSpeed * Time.deltaTime
        );

        playerCamera.localPosition = cameraPosition;

        //==========================================
        // MOVEMENT
        //==========================================

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        // Slower while crouching
        float currentSpeed = isCrouching
            ? crouchSpeed
            : speed;

        //==========================================
        // NORMAL PLAYER MOVEMENT
        //==========================================

        if (ChairInteraction.CurrentChair == null)
        {
            controller.Move(
                move.normalized *
                currentSpeed *
                Time.deltaTime
            );
        }

        //==========================================
        // CHAIR MODE
        //==========================================

        else
        {
            ChairInteraction chair =
                ChairInteraction.CurrentChair;

            Vector3 delta =
                move.normalized *
                currentSpeed *
                Time.deltaTime;

            controller.Move(delta);

            // Move chair with player
            chair.transform.position += delta;

            // Keep chair in front of player
            Vector3 target =
                transform.position +
                transform.forward *
                chair.holdDistance;

            target.y = chair.transform.position.y;

            chair.transform.position = Vector3.Lerp(
                chair.transform.position,
                target,
                chair.moveSpeed *
                Time.deltaTime
            );

            // Rotate chair with player
            chair.transform.rotation = Quaternion.Lerp(
                chair.transform.rotation,
                chair.GetTargetRotation(),
                chair.rotationSpeed *
                Time.deltaTime
            );
        }

        //==========================================
        // GRAVITY
        //==========================================

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
        );
    }
}