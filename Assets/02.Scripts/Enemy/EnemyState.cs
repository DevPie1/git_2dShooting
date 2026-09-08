using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public int CurrentHealth => health;
    protected int health;
    private ItemSpawner _itemSpawner;
    private Animator _animator;
    private float _hitSec;

    private void Start()
    {
        if (health == 0) health = 60;
        _animator = GetComponent<Animator>();

        _itemSpawner = FindFirstObjectByType<ItemSpawner>();
    }

    // 💡 중요: 자식 클래스가 본인만의 체력을 주입할 수 있는 메서드 추가
    public void SetInitialHealth(int maxHealth)
    {
        health = maxHealth;
    }

    public void ItemSpawn()
    {
        if (_itemSpawner != null)
        {
            _itemSpawner.SpawnItem(transform.position);
        }

        Debug.Log("ItemSpawn");
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage");


        _animator.SetTrigger("isHit");

        health -= damage;
        if (health <= 0)
        {
            ItemSpawn();
            Destroy(this.gameObject);
        }
    }

    public void Die()
    {
        ItemSpawn();
        Destroy(this.gameObject);
    }
}