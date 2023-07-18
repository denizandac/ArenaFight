using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float rotationSmoothVelocity, horizontalInput, verticalInput, angle, targetAngle;
    public Vector3 movementDirection;
    public Animator animator;
    public Camera mainCamera;
    public CharacterController characterController;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CalculateCharacterMovement();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        characterController.Move(movementDirection * speed * Time.deltaTime);
    }
    private void CalculateCharacterMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (moveDirection.magnitude >= 0.1f)
        {
            animator.SetBool("isWalking", true);
            targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, rotationSpeed);
            movementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            animator.SetBool("isWalking", false);
            movementDirection = Vector3.zero;
        }
    }
}
