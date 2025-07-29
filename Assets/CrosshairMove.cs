using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0f;
    // Update is called once per frame
    void Update()
    {
        Relocate();
    }
    private void Relocate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 nPos = hitInfo.point;
            transform.position = Vector3.SmoothDamp(transform.position, nPos, ref velocity, smoothTime);
        }
    }
}
