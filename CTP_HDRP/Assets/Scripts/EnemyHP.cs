using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int enmeyCurrentHealth;
    public EnemyHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        enmeyCurrentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        enmeyCurrentHealth -= damage;

        healthBar.SetHealth(enmeyCurrentHealth);
    }
}
