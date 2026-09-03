using UnityEngine;

public class TargetEnemy : EnemyMove
{
    private Vector2 _moveDirection;

    void Start()
    {
        _speed = 2f;
        if (playerTransform != null)
        {
            _moveDirection = playerTransform.position - transform.position;
            _moveDirection.Normalize();
        }
        else
        {
            // 플레이어가 없으면 그냥 아래로 이동
            _moveDirection = Vector2.down;
        }
    }

    protected override void Move()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.World);
    }
}