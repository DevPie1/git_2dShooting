using UnityEngine;

public class TargetEnemy : EnemyMove
{
    private Vector2 _moveDirection;

    [SerializeField] private int _customHealth;
    [SerializeField] private GameObject _deathEftPrefab;

    void Start()
    {
        Damage = 30;
        _speed = 4f;

        EnemyState enemyState = GetComponent<EnemyState>();
        if (enemyState != null)
        {
            enemyState.SetInitialHealth(_customHealth);
            enemyState.SetDeathEftPrefab(_deathEftPrefab);
        }
        else
        {
            Debug.LogError($"{gameObject.name}에 EnemyState 컴포넌트가 없습니다!");
        }

        if (Player != null)
        {
            playerTransform = Player.transform;
            _moveDirection = playerTransform.position - transform.position;
            _moveDirection.Normalize();
            float targetAngle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, targetAngle + 90);
        }
        else
        {
            _moveDirection = Vector2.down;
        }
    }

    protected override void Move()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.World);
    }
}