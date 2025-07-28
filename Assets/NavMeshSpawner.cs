using UnityEngine;
using System.Collections;
public class NavMeshSpawner : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] protected FollowNavMesh entity;
    [SerializeField] protected Transform target;
    [SerializeField] protected LoggerComponent logger;

    [Header("Start Settings")]
    [SerializeField] protected float maxRadius;
    [SerializeField] protected float minRadius;
    [SerializeField] protected float spawnRate;
    [SerializeField] protected float startSpeed = 5;

    [Header("Upgrade Settings")]
    [SerializeField] protected bool turnIncrease = true;
    [SerializeField] protected float timeToIncrease = 10;
    [Tooltip("0 не применяет увелечение")]
    [SerializeField] protected float speedIncrease = 0.1f;
    [Tooltip("0 не применяет уменьшение")]
    [SerializeField] protected float spawnRateDecsrease = 0.05f;

    private Coroutine scaleRoutine;
    private Coroutine spawnRoutine;

    void Start()
    {
        StartCoroutine(Spawn());
        if (turnIncrease && scaleRoutine == null) scaleRoutine = StartCoroutine(Scale());
    }
    private void Awake()
    {
        if (minRadius > maxRadius) { minRadius = maxRadius; }
        entity.target = target.gameObject;
        entity.logger = logger;
        entity.gameObject.GetComponent<Health>().logger = logger;
    }
    protected IEnumerator Spawn()
    {
        while(true){
            float randX = Random.Range(minRadius, maxRadius);
            float randZ = Random.Range(minRadius, maxRadius);
            if (1 - Random.value < 0.5) randX *= -1;
            if (1 - Random.value < 0.5) randZ *= -1;
            Vector3 spawnPos = new Vector3(randX + target.position.x, target.position.y, randZ + target.position.z);
            FollowNavMesh f = Instantiate(entity, spawnPos, Quaternion.identity);   
            f.Speed = startSpeed;
            logger?.Log($"Spawned on {spawnPos}");
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private IEnumerator Scale()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToIncrease);
            logger?.Log($"{gameObject.name} is Scaling");
            startSpeed += startSpeed * speedIncrease;
            spawnRate -= spawnRate * spawnRateDecsrease;
        }
    }

    // Update is called once per frame
}
