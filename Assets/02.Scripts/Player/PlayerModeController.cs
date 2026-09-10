using UnityEngine;

public class PlayerModeController : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerAutoMove _playerAutoMove;

    [SerializeField] private bool _startWithAuto = false; // 시작 시 오토 모드 여부

    void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerAutoMove = GetComponent<PlayerAutoMove>();
    }

    void Start()
    {
        // 시작 모드 세팅
        SetAutoMode(_startWithAuto);
    }

    void Update()
    {
        // 1. 단축키(Tab 키)로 오토 모드 토글 (켜기/끄기)
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleAutoMode();
        }

        // 2. [선택 사항 - 편의 기능] 
        // 오토 모드 중에 유저가 방향키를 직접 누르면 즉시 수동 모드로 복귀
        if (_playerAutoMove.enabled)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                SetAutoMode(false);
            }
        }
    }

    public void ToggleAutoMode()
    {
        SetAutoMode(!_playerAutoMove.enabled);
    }

    public void SetAutoMode(bool isAuto)
    {
        _playerAutoMove.enabled = isAuto;
        _playerMove.enabled = !isAuto;

        Debug.Log($"[플레이어 모드] {(isAuto ? "🤖 자동(AUTO) 모드" : "🎮 수동(MANUAL) 모드")}");
    }
}