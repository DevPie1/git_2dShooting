using UnityEngine;
using UnityEngine.Serialization;

public class EnemeySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] enemyPrefab;

    [SerializeField] private float _spawnInterval = 3f;

    private float _timer;

    private void Spawn()
    {
        if (enemyPrefab == null || enemyPrefab.Length == 0) return;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // 0.0부터 100.0 사이의 랜덤 실수 생성
            float randomChance = Random.Range(0f, 100f);
            int targetIndex = 0;


            //TODO : ScriptableObject를 사용해서 리팩토링
            // 이뮤1 : 배영을 사용했지만 각 아이템이 어떤 프리팹인지 알수가 없음
            // 이유 2 : 각 에너미 스폰 확률을 매직 넘버로 하드코딩해서 유지보수가 어렵 
            if (randomChance < 50f)
            {
                // 0 ~ 50 미만 (50% 확률) -> Downward
                targetIndex = 0;
            }
            else if (randomChance < 80f)
            {
                // 50 이상 ~ 80 미만 (30% 확률) -> Aimed
                targetIndex = 1;
            }
            else
            {
                // 80 이상 ~ 100 이하 (20% 확률) -> Homing
                targetIndex = 2;
            }

            // 결정된 인덱스의 적 프리팹 생성
            GameObject enemy = Instantiate(enemyPrefab[targetIndex]);
            enemy.transform.position = spawnPoints[i].position;
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