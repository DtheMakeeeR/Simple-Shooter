using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
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
    private bool isReloading = false;
    private Coroutine coroutine;
    private bool IsHoldingFire
    {
        get
        {
            return isHoldingFire;
        }
        set
        {
            logger?.Log($"IsHoldingFire ={value}");
            isHoldingFire = value;
        }
    }
    PlayerInputActions playerInputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        bullet.logger = logger;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Attack.started += ctx => { IsHoldingFire = true; StartShoot();  };
        playerInputActions.Player.Attack.canceled += ctx => IsHoldingFire = false;
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
    //private IEnumerator Reload()
    //{
    //    isReloading = true;
    //    yield return new WaitForSeconds(info.reloadTime);
    //    isReloading = false;
    //}
    private void StartShoot()
    {
        if (coroutine != null) return;
        else coroutine = StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        logger?.Log("Shoot");
        int flag = 0;
        if (info.isAuto) while (isHoldingFire)
            {
                flag++;
                if (flag == 100) yield break;
                if (!isReloading)
                {
                    Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                    isReloading = true;
                    yield return new WaitForSeconds(info.reloadTime);
                    isReloading = false;
                }
            }
        else if (!isReloading)
        {
            Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            isReloading = true;
            yield return new WaitForSeconds(info.reloadTime);
            isReloading = false;
        }
        coroutine = null;
    }
}
