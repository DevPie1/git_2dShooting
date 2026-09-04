using UnityEngine;

public class ChaserEnemy : EnemyMove
{
    private Vector2 _direction;

    private void Start()
    {
        _speed = 2.0f;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    protected override void Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;

            _direction = playerTransform.position - transform.position;

            if (playerTransform != null)
            {
                _direction = (playerTransform.position - transform.position).normalized;

                if (_direction.magnitude > 0.1f)
                {
                    _direction.Normalize();
                }

                transform.Translate(
                    _direction * _speed * Time.deltaTime,
                    Space.World
                );
            }
            else
            {
                transform.Translate(
                    Vector2.down * _speed * Time.deltaTime
                );
            }
        }
    }
}