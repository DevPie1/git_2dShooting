using UnityEngine;

public class NormalEnemy : EnemyMove
{
    [SerializeField] private int _customHealth = 20;

    private void Start()
    {
        _speed = 1f;
        EnemyState enemyState = GetComponent<EnemyState>();
        if (enemyState != null)
        {
            enemyState.SetInitialHealth(_customHealth);
        }
        else
        {
            Debug.LogError($"{gameObject.name}에 EnemyState 컴포넌트가 없습니다!");
        }
    }


    protected override void Move()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }
}