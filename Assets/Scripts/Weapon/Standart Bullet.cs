using System.Runtime.CompilerServices;
using UnityEngine;

public class StandartBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float range;

    [SerializeField] private LoggerComponent logger;
    public void SetValues(WeaponInfo weaponInfo)
    {
        damage = weaponInfo.damage = 1;
        speed = weaponInfo.speed;
        range = weaponInfo.range;
    }

    void Update()
    {
        Fly();
    }
    public void Fly()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.Self);
        range -= Time.deltaTime * speed;
        logger.Log($"range:{range}");   
        if (range < 0) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().GetDamage(damage);
        }
    }
}
