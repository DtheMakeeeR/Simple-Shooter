using System;
using UnityEngine;

public class CharacterControllRB : MonoBehaviour
{
    private Rigidbody rb;
    public Transform playerCamera;
    public Transform feet;
    public LayerMask groundLayer;

    private Vector2 mouseInput;
    private Vector3 movementInput;
    private float xRot;

    public float walkSpeed = 5;
    public float sprintSpeed = 10;
    public float jumpForce = 5;
    public float xSentetivity = 5;
    public float ySentetivity = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MoveCharacter();
        MoveCamera();
    }

    private void MoveCamera()
    {
        xRot -= mouseInput.y * ySentetivity;
        transform.Rotate(0f, mouseInput.x * xSentetivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    private void MoveCharacter()
    {
        Vector3 moveVector;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveVector = transform.TransformDirection(movementInput) * sprintSpeed;
        }
        else
        {
            moveVector = transform.TransformDirection(movementInput) * walkSpeed;
        }
        rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(feet.position, 0.1f, groundLayer))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("You can't jump while in the air!");
            }
        }
    }
}
