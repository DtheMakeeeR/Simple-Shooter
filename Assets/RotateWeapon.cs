using UnityEngine;
using UnityEngine.InputSystem;

public class RotateWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform crossHair;
    [SerializeField] private float headHeight;
    [SerializeField] private float bodyHeight;

    private float currHeight;
    private void Awake()
    {
        currHeight = bodyHeight;
    }
    void Update()    
    {
        Aim();
    }

    public void ChangeHeight(InputAction.CallbackContext ctx)
    {
        if(ctx.performed) currHeight = headHeight;
        else currHeight = bodyHeight;
    }
    private void Aim()
    {
        Vector3 lookPos = crossHair.position;
        lookPos.y += currHeight;
        transform.LookAt(lookPos);
    }
}
