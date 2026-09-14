using UnityEngine;
using UnityEngine.Serialization;

public class EnemeySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] enemyPrefab;

    [SerializeField] private float _spawnInterval = 3f;

    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;

    private float _timer;

    private void Spawn()
    {
        if (enemyPrefab == null || enemyPrefab.Length == 0) return;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int targetIndex = 0;
            //TODO : ScriptableObject를 사용해서 리팩토링
            // 이뮤 1 : 배열을 사용했지만 각 아이템이 어떤 프리팹인지 알수가 없음
            // 이유 2 : 각 에너미 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵 

            // 가중치 랜덤 선택(Weight
            //각 아이템에 가중치를 부여하고, 가중치가 클수록 높은 확률로 선택되도록 하는 방식

            // 1. 추첨할 수 있는 모든 가중치를 더한다.
            int totalWeight = 0;
            foreach (EnemySpawnData data in _spawnDataTable.Datas)
            {
                totalWeight += data.Weight;
            }

            // 2. 전체 가중치 범위에서 랜덤한 정수를 뽑는다.
            int randomWeight = Random.Range(0, totalWeight);

            //3. 가중치를 누적하면서 선택된 구간을 찾는다.
            int cumulativeWeight = 0;
            foreach (EnemySpawnData data in _spawnDataTable.Datas)
            {
                cumulativeWeight += data.Weight;
                if (randomWeight < cumulativeWeight)
                {
                    GameObject enemyPrefab = Instantiate(data.EnemyPrefab);
                    enemyPrefab.transform.position = spawnPoints[i].position;
                    break;
                }
            }
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _spawnInterval)
        {
            _timer = 0f;

            _spawnInterval = Random.Range(1f, 3f);

            Spawn();
        }
    }
}