using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject player;
    [Header("Настройки")]
    [Tooltip("Влияет на...")]
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private LoggerComponent logger;
    private Vector3 velocity = Vector3.zero;

    private Vector3 offset;
    
    private void Awake()
    {
        offset = transform.position;
    }
    private void CameraMove()
    {
        //Vector3 newPos = player.transform.position + offset;
        //newPos = player.transform.rotation * newPos;
        //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
        //logger?.Log($"transform.rotation.x:{transform.rotation.x}, player.transform.rotation.y:{player.transform.rotation.y}, transform.rotation.z:{transform.rotation.z}");
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z), smoothTime);
        //logger?.Log($"transform.rotation:{transform.rotation}");
        // Позиция
        Vector3 targetPos = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );

        // Поворот (камера смотрит на игрока с небольшим смещением)
        Vector3 lookDirection = player.transform.position - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            Time.deltaTime * 1f
        );
    }
    // Update is called once per frame
    void Update()
    {
        
        CameraMove();
    }
}
