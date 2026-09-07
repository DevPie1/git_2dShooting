using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Item[] _itemPrefabs;

    private float _dropChance = 0.3f;

    public void SpawnItem(Vector3 position)
    {
        // 30% 확률
        if (Random.value > _dropChance)
        {
            return;
        }

        // 3가지 아이템 중 하나 선택
        int randomIndex = Random.Range(0, _itemPrefabs.Length);

        // 아이템 생성
        Instantiate(_itemPrefabs[randomIndex], position, Quaternion.identity);
    }
}