using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    /*[SerializeField] private int damage = 1;
    [SerializeField] private int bulletsNumber = 1;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private bool isAuto = false;
    [SerializeField] private string weaponName;*/
    [SerializeField] private WeaponInfo info;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private StandartBullet bullet;

    [SerializeField] private LoggerComponent logger;

    private bool isHoldingFire = false;

    PlayerInputActions playerInputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Attack.started += ctx => { isHoldingFire = true; StartCoroutine("Shoot");  };
        playerInputActions.Player.Attack.canceled += ctx => isHoldingFire = false;
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
    }
    private void OnDisable()
    {
        playerInputActions.Disable();
    }
    void Start()
    {
        bullet.SetValues(info);
    }
    
    private IEnumerator Shoot()
    {
        logger.Log("Shoot");
        if (info.isAuto) while (isHoldingFire)
            {
                Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                yield return new WaitForSeconds(info.reloadTime);
            }
        else
        { 
            Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            yield return new WaitForSeconds(info.reloadTime);
        }
    }
}
