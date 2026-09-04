using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int health;

    private void Start()
    {
        health = 100;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}