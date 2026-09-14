using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] private Item[] _itemPrefabs;

    private float _dropChance = 0.3f;

    [SerializeField] private ItemSpawnDataTableSO _itemSpawnDataTable;

    public void SpawnItem(Vector3 position)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int totalWeight = 0;

            foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
            {
                totalWeight += data.Weight;
            }

            int randomWeight = Random.Range(0, totalWeight);

            int cumulativeWeight = 0;

            foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
            {
                cumulativeWeight += data.Weight;
                if (randomWeight < cumulativeWeight)
                {
                    GameObject itemPrefab = Instantiate(data.itemPrefab);
                    itemPrefab.transform.position = spawnPoints[i].position;
                    break;
                }
            }
        }

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