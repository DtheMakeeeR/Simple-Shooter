using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
public class Shooting : MonoBehaviour
{
    [SerializeField] private WeaponInfo info;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private StandartBullet bullet;
    [SerializeField] private LoggerComponent logger;

    private bool isHoldingFire = false;
    private bool isReloading = false;
    private Coroutine coroutine;
    private int clipSize;

    PlayerInputActions playerInputActions;
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
    #region messages
    private void Awake()
    {
        bullet.logger = logger;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Attack.started += ctx => { IsHoldingFire = true; StartShoot();  };
        playerInputActions.Player.Attack.canceled += ctx => IsHoldingFire = false;
        playerInputActions.Player.Reload.started += ctx => StartReload();
        clipSize = info.clipSize;
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
    #endregion
    private void StartShoot()
    {
        if (coroutine != null) return;
        else coroutine = StartCoroutine(Shoot());
    }
    private void StartReload()
    {
        if (coroutine != null && !isReloading) StopCoroutine(coroutine);
        if (coroutine != null && isReloading) return;
        coroutine = StartCoroutine(Reload());
    }
    private IEnumerator Reload()
    {
        logger?.Log("Start Reload");
        isReloading = true;
        yield return new WaitForSeconds(info.reloadSpeed);
        isReloading = false;
        clipSize= info.clipSize;
        logger?.Log("End Reload");
        coroutine = null;
    }
    private IEnumerator Shoot()
    {
        logger?.Log("Shoot");
        if (info.isAuto) while (isHoldingFire && clipSize != 0)
            {
                MakeBullets();
                yield return new WaitForSeconds(info.shootingSpeed);
            }
        else if (clipSize != 0) 
        {
            MakeBullets();
            yield return new WaitForSeconds(info.shootingSpeed);
        }
        coroutine = null;
    }
    private void MakeBullets()
    {
        Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        for (int i = 1; i < info.bulletsNumber; i++)
        {
            float spreadAngle = info.spreadRadius * i;
            Quaternion spreadRotation = Quaternion.Euler(0, spreadAngle, 0);
            Quaternion finalRotation = bulletPos.rotation * spreadRotation;
            Instantiate(bullet, bulletPos.position, finalRotation);
            spreadAngle = -info.spreadRadius * i;
            spreadRotation = Quaternion.Euler(0, spreadAngle, 0);
            finalRotation = bulletPos.rotation * spreadRotation;
            Instantiate(bullet, bulletPos.position, finalRotation);
        }
        clipSize--;
        logger?.Log($"Remaining bullets:{clipSize}");
    }
}
