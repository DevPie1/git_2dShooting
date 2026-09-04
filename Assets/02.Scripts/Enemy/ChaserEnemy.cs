using UnityEngine;

public class ChaserEnemy : EnemyMove
{
    private GameObject _player;

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
        if (_player != null)
        {
            playerTransform = _player.transform;

            Vector2 direction = playerTransform.position - transform.position;

            if (direction.magnitude > 0.1f)
            {
                direction.Normalize();
            }

            transform.Translate(
                direction * _speed * Time.deltaTime,
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