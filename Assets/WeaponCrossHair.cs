using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponCrossHair : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0f;
    [SerializeField] private LayerMask ignore;
    // Update is called once per frame
    void Update()
    {
        Relocate();
    }
    private void Relocate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        LayerMask filteredMask = ~ignore;
        if (Physics.Raycast(ray, out RaycastHit hitInfo, filteredMask))
        {
            Vector3 nPos = hitInfo.point;
            transform.position = Vector3.SmoothDamp(transform.position, nPos, ref velocity, smoothTime);
        }
    }
}
