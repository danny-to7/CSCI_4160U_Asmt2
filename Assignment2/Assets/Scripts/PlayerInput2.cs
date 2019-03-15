using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput2 : MonoBehaviour {
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpFactor = 5.0f;
    [SerializeField] private AnimationCurve jumpCurve;

    private CharacterController controller;

    private bool isJumping = false;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        // movement and strafing
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        float vertical = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertical;
        Vector3 rightMovement = transform.right * horizontal;

        controller.SimpleMove(forwardMovement + rightMovement);

        // jumping
        if (Input.GetButtonDown("Jump") && !isJumping) {
            isJumping = true;
            StartCoroutine(Jump());
        }

    }

    // co-routine
    private IEnumerator Jump() {
        float timeInAir = 0.0f;

        do {
            // update the position
            float jumpAmount = jumpCurve.Evaluate(timeInAir) * jumpFactor * Time.deltaTime;
            controller.Move(Vector3.up * jumpAmount);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }
}
