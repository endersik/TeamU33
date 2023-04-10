using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Animator _animator;
        
        public int maxHealth = 100;
    
        private int currentHealth;
    
        private void Start()
        {
            currentHealth = maxHealth;
        }
    
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            
            _animator.SetTrigger("Hurt");
    
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    
        void Die()
        {
            Debug.Log("Enemy Died");
            
            _animator.SetBool("Dead",true);
            
            Destroy(gameObject);
        }
}
