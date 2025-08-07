using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponCrossHair : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float sensetivity = 0.1f;
    [SerializeField] private LayerMask ignore;
    // Update is called once per frame
    void Update()
    {
    }
    public void Relocate(InputAction.CallbackContext context)
    {
        Vector2 mouaseDelta = context.ReadValue<Vector2>();
        Vector3 direction = new Vector3(mouaseDelta.x, transform.position.y, mouaseDelta.y);
        transform.Translate(direction* sensetivity);
    }
}
