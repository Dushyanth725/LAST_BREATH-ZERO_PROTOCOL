using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //==========================================
        // NORMAL PLAYER MOVEMENT
        //==========================================

        if (ChairInteraction.CurrentChair == null)
        {
            controller.Move(move.normalized * speed * Time.deltaTime);
        }

        //==========================================
        // CHAIR MODE
        //==========================================

        else
        {
            ChairInteraction chair = ChairInteraction.CurrentChair;

            // Move BOTH player and chair
            Vector3 delta = move.normalized * speed * Time.deltaTime;

            controller.Move(delta);

            chair.transform.position += delta;

            // Keep chair in front of player
            Vector3 target =
                transform.position +
                transform.forward * chair.holdDistance;

            target.y = chair.transform.position.y;

            chair.transform.position = Vector3.Lerp(
                chair.transform.position,
                target,
                chair.moveSpeed * Time.deltaTime);

            // Rotate chair with player
            chair.transform.rotation = Quaternion.Lerp(
                chair.transform.rotation,
                chair.GetTargetRotation(),
                chair.rotationSpeed * Time.deltaTime);
        }

        //==========================================

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}