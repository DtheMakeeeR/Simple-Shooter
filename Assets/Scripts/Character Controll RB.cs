using System;
using UnityEngine;

public class CharacterControllRB : MonoBehaviour
{
    private Rigidbody rb;
    private Transform playerCamera;

    private Vector2 mouseRotation;
    private Vector3 moveDirection;

    public float walkSpeed = 1;
    public float sprintSpeed = 2;
    public float jumpForce = 3;
    public float xSentetivity;
    public float ySentetivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift)) moveDirection *= sprintSpeed;
        else moveDirection *= walkSpeed;
        rb.MovePosition(moveDirection * Time.deltaTime);
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
    }
}
