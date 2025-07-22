using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Follow entity;
    [SerializeField] private Transform target;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    [SerializeField] private float spawnRate;
    [Header("Logger")]
    [SerializeField] private LoggerComponent logger;
    private void Awake()
    {
        if (minRadius > maxRadius) { minRadius = maxRadius; }
        entity.target = target.gameObject;
        entity.logger = logger;
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
            logger?.Log($"Spawned on {spawnPos}");
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Spawn());
    }

}
