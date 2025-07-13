using System;
using UnityEngine;
using Unity.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    int health = 3;
    public event Action OnHit;
    TMP_Text t;
    void Start()
    {
        t = GetComponentInChildren<TMP_Text>();
        t.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetDamage(int damage)
    {
        health-=damage;
        if (t != null)
        {
            t.text = health.ToString();
        }
    }
}
