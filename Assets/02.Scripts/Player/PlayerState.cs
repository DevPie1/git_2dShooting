using UnityEngine;
using UnityEngine.Serialization;

public class PlayerState : MonoBehaviour
{
    public int health;
    [SerializeField] public float AttackSpeed = 1f;
    public float _moveSpeed = 5f;

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

    public void IncreaseAttackSpeed(float amount)
    {
        AttackSpeed += amount;

        Debug.Log($"공격 속도 증가: {AttackSpeed}");
    }

    public void IncreaseHealth(int amount)
    {
        health += amount;

        Debug.Log($"체력 증가: {health}");
    }

    public void IncreaseMoveSpeed(float amount)
    {
        _moveSpeed += amount;

        Debug.Log($"이동 속도 증가: {_moveSpeed}");
    }
}