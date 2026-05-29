using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour
{
    public HealthbarBehaviour Healthbar;
    public float Hitpoints;
    public float MaxHitpoints = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}