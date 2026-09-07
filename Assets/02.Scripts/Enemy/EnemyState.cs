using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private ItemSpawner _itemSpawner;

    private void Start()
    {
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

    public void Die()
    {
        ItemSpawn();
        Destroy(this.gameObject);
    }
}