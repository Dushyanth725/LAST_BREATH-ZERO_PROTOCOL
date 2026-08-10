using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2.75f;
    public float gravity = -9.81f;

    [Header("Crouch")]
    public float normalHeight = 1.8f;
    public float crouchHeight = 0.95f;
    public float crouchTransitionSpeed = 8f;

    private CharacterController controller;
    private Vector3 velocity;

    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Keep your normal CharacterController setup
        controller.height = normalHeight;
    }

    void Update()
    {
        //==========================================
        // CROUCH / STAND
        //==========================================

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
        }

        //==========================================
        // HEIGHT
        //==========================================

        float targetHeight;

        if (isCrouching)
            targetHeight = crouchHeight;
        else
            targetHeight = normalHeight;

        controller.height = Mathf.Lerp(
            controller.height,
            targetHeight,
            crouchTransitionSpeed * Time.deltaTime
        );

        //==========================================
        // MOVEMENT
        //==========================================

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Slower when crouching
        float currentSpeed = isCrouching ? crouchSpeed : speed;

        //==========================================
        // NORMAL MOVEMENT
        //==========================================

        if (ChairInteraction.CurrentChair == null)
        {
            controller.Move(
                move.normalized * currentSpeed * Time.deltaTime
            );
        }

        //==========================================
        // CHAIR MODE
        //==========================================

        else
        {
            ChairInteraction chair = ChairInteraction.CurrentChair;

            Vector3 delta =
                move.normalized * currentSpeed * Time.deltaTime;

            controller.Move(delta);

            chair.transform.position += delta;

            Vector3 target =
                transform.position +
                transform.forward * chair.holdDistance;

            target.y = chair.transform.position.y;

            chair.transform.position = Vector3.Lerp(
                chair.transform.position,
                target,
                chair.moveSpeed * Time.deltaTime
            );

            chair.transform.rotation = Quaternion.Lerp(
                chair.transform.rotation,
                chair.GetTargetRotation(),
                chair.rotationSpeed * Time.deltaTime
            );
        }

        //==========================================
        // GRAVITY
        //==========================================

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}