using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Follow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] public GameObject target;
    [SerializeField] public float speed = 5f;
    [SerializeField] private int damage = 1;
    [Header("Logger")]
    [SerializeField] public LoggerComponent logger;
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        animator?.SetBool("Moving", true);
        //Debug.Log($"Target pos:{target.transform.position}");
        Vector3 tPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(tPos - transform.position);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            speed/5 * Time.deltaTime
        );
        Vector3 direction = (tPos - transform.position).normalized * speed;
        direction.y = transform.position.y > 0 ? -1 : 0;
        rb.AddForce(direction * Time.deltaTime, ForceMode.VelocityChange);
        //rb.linearVelocity = direction;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>()?.GetDamage(damage);
        }
    }
}
