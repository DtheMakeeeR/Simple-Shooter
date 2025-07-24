using System.Runtime.CompilerServices;
using UnityEngine;

public class StandartBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float force;

    [SerializeField] public LoggerComponent logger;
    public void SetValues(WeaponInfo weaponInfo)
    {
        damage = weaponInfo.damage;
        speed = weaponInfo.speed;
        range = weaponInfo.range;
        force = weaponInfo.force;
    }

    void Update()
    {
        Fly();
    }
    public void Fly()
    {
        transform.Translate(new Vector3(0, 0, 3) * speed * Time.deltaTime, Space.Self);
        range -= Time.deltaTime * speed;
        if (range < 0) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        logger?.Log("OnTriggerEnter");
        if (other.gameObject.CompareTag("Enemy"))
        {
            logger?.Log("OnTriggerEnter == Enemy");
            other.gameObject.GetComponent<Health>().GetDamage(damage);
        }
        if(other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
        }
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
