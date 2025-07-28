using UnityEngine;
using UnityEngine.InputSystem;

public class RotateWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float rotationSpeed = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.DrawRay(Camera.main.transform.position, hit.point - Camera.main.transform.position);
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        Debug.DrawRay(transform.position, hit.point - transform.position);
    }    
}
