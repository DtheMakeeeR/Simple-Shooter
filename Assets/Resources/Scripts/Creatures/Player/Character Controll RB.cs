using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterControllRB : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform crossHair;
    [SerializeField] private Transform feet;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float sprintSpeed = 10;
    [SerializeField] private float dashPower;
    [SerializeField] private float rotationSpeed;
    private bool sprint;

    [Header("Jump Settings")]
    public float jumpForce = 5;


    public int damage = 1;
    private Vector2 mouseInput;
    private Vector3 movementInput;
    PlayerInputActions playerInputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        //playerInputActions.Player.Move.performed += CalcInput;
        //playerInputActions.Player.Move.canceled += CalcInput;
        //playerInputActions.Player.Jump.performed += Jump;
        //playerInputActions.Player.Sprint.performed += ctx => sprint = true;
        //playerInputActions.Player.Sprint.canceled += ctx => sprint = false;
        //playerInputActions.Player.Attack.performed += Shoot;
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        if(ctx.performed) sprint = true;
        else sprint = false;
    }
    public void HideCursor(InputAction.CallbackContext ctx)
    {
        if(Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    public void Jump(InputAction.CallbackContext ctx)
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

    private void OnEnable()
    {
        playerInputActions.Enable();
    }
    private void OnDisable()
    {
        playerInputActions.Disable();
    }
    private void FixedUpdate()
    {
        MoveCharacter();
        TurnToMouse();
    }

    // Update is called once per frame

    private void TurnToMouse()
    {

        //if (!IsMouseInsideWindow() ) return;
        //Ray cRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Vector3 nPos = transform.position;
        //if (Physics.Raycast(cRay, out RaycastHit hit))
        //{
        //    nPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //    Vector3 direction = nPos - transform.position;
        //    Quaternion targetRotation = Quaternion.LookRotation(nPos - transform.position);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //}
        Vector3 nPos = crossHair.transform.position;
        nPos.y = transform.position.y;
        transform.LookAt(nPos, Vector3.up);
    }
    public void CalcInput(InputAction.CallbackContext ctx)
    {
        movementInput.x = ctx.ReadValue<Vector2>().x;
        movementInput.z = ctx.ReadValue<Vector2>().y;
    }
    private bool IsMouseInsideWindow()
    {
        return Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
               Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;
    }
    private void MoveCharacter()
    {
        Vector3 moveVector;
        if (sprint)
        {
            moveVector = (movementInput) * sprintSpeed;
        }
        else
        {
            moveVector = (movementInput) * walkSpeed;
        }
        //moveVector = transform.TransformDirection(moveVector);
        rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);
    }
}
