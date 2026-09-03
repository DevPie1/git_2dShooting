using UnityEngine;

public class NormalEnemy : EnemyMove
{
    private void Start()
    {
        _speed = 1f;
    }


    protected override void Move()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }
}