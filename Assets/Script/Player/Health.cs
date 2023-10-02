using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthPoint;
    public bool CanTakeDamage;

    public void TakeDamage(int Damage)
    {
        if (CanTakeDamage)
        {
            healthPoint -= Damage;

            if (healthPoint <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeHealth(int HealthPoint)
    {
        healthPoint += HealthPoint;
    }
}
