using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private Animator _animator;
    [SerializeField] private RectTransform _restrictArea;

    private float _changeAmount = 0.2f;

    private PlayerState _playerState;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerState = GetComponent<PlayerState>();
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

    private void Update()
    {
        SpeedChange();

        Move();
    }

    //1. 키보드 입력을 받는다.
    private void Move()
    {
        //Vector2 direction = new Vector2(-1, 0);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(h, v).normalized;

        _animator.SetInteger("x", (int)direction.x);
        //Debug.Log("왼쪽 방향키를 누르는 중");
        //매직 넘버란? : 보는 사람에 따라 의미가 달라질 수 있는 숫자 
        Vector3 movement = direction * _playerState._moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        _trailRenderer.emitting = direction != Vector2.zero;

        Vector3 currentPos = transform.position;
        // UI 이미지의 네 모서리 월드 좌표를 가져옵니다.
        Vector3[] corners = new Vector3[4];
        _restrictArea.GetWorldCorners(corners);


        float minX = corners[0].x;
        float maxX = corners[2].x;
        float minY = corners[0].y;
        float maxY = corners[2].y;

        // 세모가 왼쪽 끝(minX)보다 더 나가면 오른쪽 끝(maxX)으로 이동
        if (currentPos.x > maxX)
        {
            currentPos.x = minX;
        }

        else if (currentPos.x < minX)
        {
            currentPos.x = maxX;
        }

        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);

        transform.position = currentPos;
    }
}