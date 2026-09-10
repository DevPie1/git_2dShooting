using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private RectTransform _restrictArea;
    [SerializeField] private bool _isAutoMode = true; // 자동/수동 전환용 플래그
    [SerializeField] private string _enemyTag = "Enemy";

    [SerializeField] private float _dangerDistance = 2.5f; // 이 거리보다 가까우면 도망침 (생존 우선)
    [SerializeField] private float _attackDistance = 4.0f; // 적을 유지하고 싶은 적정 교전 거리
    [SerializeField] private float _avoidWeight = 2.0f; // 회피 가중치 (생존 우선순위)
    [SerializeField] private float _chaseWeight = 1.0f; // 추적 가중치

    private Animator _animator;
    private PlayerState _playerState;
    private float _changeAmount = 0.2f;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        SpeedChange();

        // 탭(Tab) 키로 수동/자동 전환 테스트 가능
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isAutoMode = !_isAutoMode;
            Debug.Log($"자동 이동 모드: {_isAutoMode}");
        }

        Move();
    }

    private void SpeedChange()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _playerState._moveSpeed *= _changeAmount * 10;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _playerState._moveSpeed *= _changeAmount;
        }
    }

    private void Move()
    {
        Vector2 direction;

        if (_isAutoMode)
        {
            // 1. 자동 이동 방향 계산
            direction = CalculateAutoMoveDirection();
        }
        else
        {
            // 2. 기존 수동 키보드 입력
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            direction = new Vector2(h, v).normalized;
        }

        // 애니메이터 설정
        _animator.SetInteger("x", Mathf.RoundToInt(direction.x));

        // 실제 이동 처리
        Vector3 movement = (Vector3)direction * _playerState._moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // 트레일 렌더러
        _trailRenderer.emitting = direction != Vector2.zero;

        // 화면 밖 제한 로직 유지
        ClampPositionWithinArea();
    }

    /// <summary>
    /// 적들을 탐색하고 생존과 공격을 고려한 최적의 이동 방향을 반환합니다.
    /// </summary>
    private Vector2 CalculateAutoMoveDirection()
    {
        // 1. Find 계열 메서드로 적 찾기 (태그 사용)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_enemyTag);

        // 씬에 적이 없으면 정지
        if (enemies.Length == 0) return Vector2.zero;

        Vector2 currentPos = transform.position;
        Vector2 avoidVector = Vector2.zero;
        GameObject closestEnemy = null;
        float minDistance = float.MaxValue;

        // 2. 모든 적을 순회하며 위험 분석 및 가장 가까운 적 탐색
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float dist = Vector2.Distance(currentPos, enemy.transform.position);

            // 가장 가까운 타겟 갱신 (공격 기준)
            if (dist < minDistance)
            {
                minDistance = dist;
                closestEnemy = enemy;
            }

            // 생존(회피): 위험 거리 안으로 들어온 모든 적에 대해 반대 방향 힘 누적
            if (dist < _dangerDistance && dist > 0.01f)
            {
                Vector2 diff = currentPos - (Vector2)enemy.transform.position;
                // 가까울수록 도망치는 힘을 더 강하게 반비례 적용
                avoidVector += (diff.normalized / dist);
            }
        }

        // 3. 공격 타겟 방향 계산 (가장 가까운 적 기준)
        Vector2 chaseVector = Vector2.zero;
        if (closestEnemy != null)
        {
            Vector2 toTarget = (Vector2)closestEnemy.transform.position - currentPos;

            // 적정 교전 거리보다 멀면 접근하고, 너무 가까우면 뒤로 물러서서 거리 유지
            if (minDistance > _attackDistance)
            {
                chaseVector = toTarget.normalized; // 접근
            }
            else
            {
                chaseVector = -toTarget.normalized * 0.5f; // 거리 벌리기
            }
        }

        // 4. 생존(회피)과 공격(접근) 벡터 합성
        Vector2 finalDirection = (avoidVector * _avoidWeight) + (chaseVector * _chaseWeight);

        return finalDirection.normalized;
    }

    /// <summary>
    /// UI RectTransform 영역 안으로 위치를 제한/랩핑하는 로직
    /// </summary>
    private void ClampPositionWithinArea()
    {
        Vector3 currentPos = transform.position;
        Vector3[] corners = new Vector3[4];
        _restrictArea.GetWorldCorners(corners);

        float minX = corners[0].x;
        float maxX = corners[2].x;
        float minY = corners[0].y;
        float maxY = corners[2].y;

        // X축 화면 랩핑 (Wrap-around)
        if (currentPos.x > maxX)
        {
            currentPos.x = minX;
        }
        else if (currentPos.x < minX)
        {
            currentPos.x = maxX;
        }

        // Y축 클램핑
        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);

        transform.position = currentPos;
    }

    // AI 감지 반경을 씬 뷰에서 시각화 (디버깅용)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _dangerDistance); // 회피 위험 구역

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackDistance); // 교전 적정 구역
    }
}