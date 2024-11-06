using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Target : MonoBehaviour
{
    public int health = 50;

    private Animation_Points points;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        points = GameObject.Find("GameManager").GetComponent<Animation_Points>();
        points.Score(1);
        Destroy(gameObject);
    }
}
