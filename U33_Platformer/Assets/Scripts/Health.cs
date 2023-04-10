using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image[] hearts;

    public int health = 5;

    private int MAX_HEALTH = 5;

    public void Damage(int amount)
    {
        hearts[health - 1].enabled = false;

        health -= amount;
    }

    public void Regen(int amount)
    {
        health += amount;

        for (int i = 0; i < health; i++)
        {
            hearts[i].enabled = true;
        }
    }

    private void Update()
    {
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (health > 0)
            {
                Damage(1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Healer"))
        {
            if (health < MAX_HEALTH)
            {
                Regen(1);
            }
        }
        
        Destroy(other.gameObject);
    }
}
