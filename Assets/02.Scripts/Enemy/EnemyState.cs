using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    private ItemSpawner _itemSpawner;
    protected int health;

    private void Start()
    {
        health = 100;
        _itemSpawner = FindFirstObjectByType<ItemSpawner>();
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