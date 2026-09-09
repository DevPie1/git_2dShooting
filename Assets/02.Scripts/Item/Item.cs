using UnityEngine;
using UnityEngine.Serialization;

public abstract class Item : MonoBehaviour
{
    private float _waitTime = 2f;
    private float _moveSpeed = 5f;
    private float _curveHeight = 2f;
    private Transform _player;

    private float _waitTimer;
    private float _moveTimer;
    private float _moveDuration;

    private bool _isMoving;

    private Vector3 _startPosition;
    private Vector3 _controlPoint;
    private Vector3 _targetPosition;

    [SerializeField] private GameObject _itemPrefab;

    private void Start()
    {
        _startPosition = transform.position;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            this._player = player.transform;
        }
    }

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        if (!_isMoving)
        {
            Wait();
            return;
        }

        MoveToPlayer();
    }

    private void Wait()
    {
        _waitTimer += Time.deltaTime;

        if (_waitTimer >= _waitTime)
        {
            StartMove();
        }
    }

    private void StartMove()
    {
        _isMoving = true;

        _moveTimer = 0f;

        _startPosition = transform.position;
        _targetPosition = _player.position;

        _controlPoint = (_startPosition + _targetPosition) * 0.5f + Vector3.up * _curveHeight;

        float distance = Vector3.Distance(
            _startPosition,
            _targetPosition
        );

        _moveDuration = distance / _moveSpeed;
    }

    private void MoveToPlayer()
    {
        _moveTimer += Time.deltaTime;

        // 이동 비율 (0에서 1까지)
        float t = Mathf.Clamp01(_moveTimer / _moveDuration);

        // 🌟 플레이어가 움직이므로 타겟 위치를 실시간으로 갱신합니다.
        _targetPosition = _player.position;

        // 🌟 시작점과 움직인 타겟점 사이의 중간점과 제어점을 매 프레임 재계산합니다.
        _controlPoint = (_startPosition + _targetPosition) * 0.5f + Vector3.up * _curveHeight;

        // 변경된 제어점을 바탕으로 베지에 곡선 위치를 계산합니다.
        transform.position = CalculateBezierPoint(t, _startPosition, _controlPoint, _targetPosition);
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 start, Vector3 control, Vector3 end)
    {
        float oneMinusT = 1f - t;

        return oneMinusT * oneMinusT * start + 2f * oneMinusT * t * control + t * t * end;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerState player = other.GetComponent<PlayerState>();

        if (player != null)
        {
            Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            ApplyEffect(player);
        }

        Destroy(gameObject);
    }

    protected abstract void ApplyEffect(PlayerState player);
}