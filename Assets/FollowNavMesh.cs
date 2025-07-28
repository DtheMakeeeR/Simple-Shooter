using UnityEngine;
using UnityEngine.AI;
public class FollowNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] public GameObject target;

    [Header("Logger")]
    [SerializeField] public LoggerComponent logger;

    public float Speed
    {
        get => agent.speed;
        set => agent.speed = value;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        if (agent != null)
        {
            agent.SetDestination(target.transform.position);
            if (agent.velocity.magnitude != 0) animator.SetBool("Moving", true);
            else animator.SetBool("Moving", false);
        }
        else
        {
            logger?.Log("No Agent");
        }
    }
    private void OnAnimatorMove()
    {
        //if (animator.GetBool("Moving"))
        //{
        //    agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        //}
    }
}
