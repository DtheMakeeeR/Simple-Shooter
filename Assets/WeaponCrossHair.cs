using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WeaponCrossHair : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float sensetivity = 0.1f;
    [SerializeField] private LayerMask ignore;
    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }
    public void Relocate(InputAction.CallbackContext context)
    {
        Vector2 mouaseDelta = context.ReadValue<Vector2>();
        Vector3 direction = new Vector3(mouaseDelta.x, transform.position.y, mouaseDelta.y);
        transform.Translate(direction* sensetivity);
    }
    private void CheckGround()
    {
        Vector3 origin = transform.position + Vector3.up*10;
        if (Physics.Raycast(origin, -Vector3.up, out RaycastHit hit, Mathf.Infinity, ignore))
        {
            transform.position = hit.point;
        }
    }
}
