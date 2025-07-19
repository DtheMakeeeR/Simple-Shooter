using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Follow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private GameObject target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    [Header("Logger")]
    [SerializeField] private LoggerComponent logger;
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        animator?.SetBool("Moving", true);
        //Debug.Log($"Target pos:{target.transform.position}");
        transform.LookAt(target.transform.position, Vector3.up);
        Vector3 direction = transform.forward * speed;
        direction.y = 0;
        rb.linearVelocity = direction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>()?.GetDamage(damage);
        }
    }
}
