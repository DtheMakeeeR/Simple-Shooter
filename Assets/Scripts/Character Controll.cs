using Unity.VisualScripting;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] Transform playerCamera;
    [Space]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [Space]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    [Space]
    [SerializeField] private float xSentetivity = 5f;
    [SerializeField] private float ySentetivity = 5f;
    private float xRot;
    private Vector2 mouseInput;
    private Vector3 movementInput;
    private Vector3 velocity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        MovePlayer();
        MoveCamera();
    }
    private void MovePlayer()
    {
        Vector3 moveDirection = transform.TransformDirection(movementInput);
        if(cc.isGrounded)
        {
            velocity.y = -1f;
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("You jumped");
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }
        cc.Move(moveDirection * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed) * Time.deltaTime);
        cc.Move(velocity * Time.deltaTime);
    }
    private void MoveCamera()
    {
        xRot -= mouseInput.y * ySentetivity;
        transform.Rotate(0f, mouseInput.x * xSentetivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(Mathf.Clamp(xRot, -90, 90), 0f, 0f);
    }
}
