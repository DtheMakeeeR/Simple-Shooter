using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject player;
    [Range(0.1f, 1f)]
    [Header("���������")]
    [Tooltip("������ ��...")]
    [SerializeField] private float ratio;
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private Vector3 offset;
    
    private void Awake()
    {
        offset = transform.position;
    }
    private void CameraMove()
    {
        //Vector3 newPos = player.transform.position + offset;
        //transform.position = Vector3.Lerp(transform.position,newPos, Time.deltaTime * ratio);
        Vector3 newPos = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }
    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }
}
