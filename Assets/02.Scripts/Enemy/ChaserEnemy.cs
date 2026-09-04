using UnityEngine;

public class ChaserEnemy : EnemyMove
{
    private void Start()
    {
        _speed = 2.0f;
        Damage = 40;

        if (Player != null)
        {
            playerTransform = Player.transform;
        }
    }

    protected override void Move()
    {
        if (Player != null)
        {
            playerTransform = Player.transform;

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