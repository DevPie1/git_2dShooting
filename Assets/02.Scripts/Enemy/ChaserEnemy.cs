using UnityEngine;

public class ChaserEnemy : EnemyMove
{
    private void Start()
    {
        _speed = 2.0f;
    }

    protected override void Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector2.down * _speed * Time.deltaTime);
        }
    }
}