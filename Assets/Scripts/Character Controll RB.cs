using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllRB : MonoBehaviour
{
    private Rigidbody rb;
    public Transform playerCamera;
    public Transform feet;
    public LayerMask groundLayer;

    private Vector2 mouseInput;
    private Vector3 movementInput;
    private float xRot;

    private LayerMask m;
    public float walkSpeed = 5;
    public float sprintSpeed = 10;
    public float jumpForce = 5;
    public float xSentetivity = 5;
    public float ySentetivity = 5;
    public int damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m =  LayerMask.NameToLayer("enemy");
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
       // mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MoveCharacter();
        // MoveCamera();
        TurnToMouse();
        Shoot();
    }

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.forward * 0.5f, transform.forward, out hit))
            {
                Debug.DrawRay(transform.position + transform.forward * 0.5f, transform.forward * hit.distance, Color.green, 1f);
                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy e = hit.collider.GetComponent<Enemy>();
                    e?.GetDamage(damage);
                }
                else
                {
                    Debug.Log("Попал не во врага, а в: " + hit.collider.gameObject.name + " с тегом: " + hit.collider.tag);
                }
            }
        }
    }
    private void TurnToMouse()
    {
        Ray cRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 nPos = transform.position;
        if (Physics.Raycast(cRay, out hit, Mathf.Infinity))
        {
            nPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            Quaternion nRot = Quaternion.FromToRotation(transform.forward, transform.TransformDirection(nPos));
            transform.rotation = Quaternion.Slerp(transform.rotation, nRot, 5f * Time.deltaTime);
            //Debug.Log($"nPos:{nPos}");
        }
        transform.LookAt(nPos, Vector3.up);
    }
    private void OnMove(InputValue inputValue)
    {
        movementInput.x = inputValue.Get<Vector2>().x;
        movementInput.z = inputValue.Get<Vector2>().y;
        Debug.Log($"OnMove: {movementInput}");
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
            moveVector = (movementInput) * sprintSpeed;
        }
        else
        {
            moveVector = (movementInput) * walkSpeed;
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
