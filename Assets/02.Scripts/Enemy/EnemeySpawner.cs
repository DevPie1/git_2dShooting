using UnityEngine;

public class EnemeySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject[] _enemyPrefab;

    //Spawner : 위치 값 에서 생성 
    private void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab[i]);
            enemy.transform.position = spawnPoints[i].position;
        }
    }
}