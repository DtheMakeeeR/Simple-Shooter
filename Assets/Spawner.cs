using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Follow entity;
    [SerializeField] private Transform target;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    [SerializeField] private float spawnRate;
    [SerializeField] private float factor = 10;
    [SerializeField] private float startSpeed = 5;
    [Header("Logger")]
    [SerializeField] private LoggerComponent logger;
    private void Awake()
    {
        if (minRadius > maxRadius) { minRadius = maxRadius; }
        entity.target = target.gameObject;
        entity.logger = logger;
        entity.gameObject.GetComponent<Health>().logger = logger;
    }
    private IEnumerator Spawn()
    {
        while(true){
            float randX = Random.Range(minRadius, maxRadius);
            float randZ = Random.Range(minRadius, maxRadius);
            if (1 - Random.value < 0.5) randX *= -1;
            if (1 - Random.value < 0.5) randZ *= -1;
            Vector3 spawnPos = new Vector3(randX + target.position.x, target.position.y, randZ + target.position.z);
            Follow f = Instantiate(entity, spawnPos, Quaternion.identity);
            f.speed = startSpeed;
            logger?.Log($"Spawned on {spawnPos}");
            yield return new WaitForSeconds(spawnRate);
        }
    }
    private IEnumerator Scale()
    {
        while(true)
        { 
            yield return new WaitForSeconds(factor);
            logger?.Log($"{gameObject.name} is Scaling");
            startSpeed *= 1.1f;
            spawnRate -= spawnRate * 0.05f;
        }
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(Scale());
    }

}
