using System;
using UnityEngine;
using Unity.UI;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    [Header("Logger")]
    [SerializeField] private LoggerComponent logger;
    [Header("Animator")]
    [SerializeField] private Animator animator;
    private int health;
    public event Action OnHit;
    private void Awake()
    {
        health = maxHealth;
    }
    public void GetDamage(int damage)
    {
        health-=damage;
        logger?.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        logger.Log($"{gameObject.name} has died.");
        if (animator != null)
        {
            animator.SetTrigger("Die");
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            logger.LogWarning("Animator component not found on " + gameObject.name);
        }
    }
}
