using UnityEngine;

public class ChaserEnemy : EnemyMove
{
    [SerializeField] private float _rotationSpeed = 10.0f;

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
        playerTransform = Player.transform;

        Vector2 direction = playerTransform.position - transform.position;

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();

            // 🔄 [추가] 플레이어 방향으로 회전하는 로직
            // direction 방향의 각도(라디안)를 계산 후 도(Degree) 단위로 변환합니다.
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle + 90);

            // 부드럽게 회전하고 싶다면 Lerp 사용
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            // 만약 즉시 딱딱 바라보게 하고 싶다면 아래 주석을 해제하고 위 Lerp를 지우세요.
            // transform.rotation = targetRotation;
        }

        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}