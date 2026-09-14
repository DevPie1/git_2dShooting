using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI_AutoButton : MonoBehaviour
{
    private Image _myImage;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    private bool _autoMode = false;
    private PlayerState _playerState;
    private AudioSource _audioSource;

    [SerializeField] private AnimationCurve _bumpCurve;

    private float _scale = 1.0f;
    private bool _isBumping = false;
    private float _elapsedTime = 0.0f;
    private const float BumpDuration = 0.3f;
    private const float OriginScale = 1.0f;
    private const float BumpScale = 1.1f;

    void Start()
    {
        _myImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
        _playerState = GameObject.FindAnyObjectByType<PlayerState>();

        AutoTooggle();
    }

    public void AutoTooggle()
    {
        _autoMode = !_autoMode;

        _playerState.GetComponent<PlayerFire>().SetAuto(_autoMode);
        _playerState.GetComponent<PlayerMove>().enabled = !_autoMode;
        _playerState.GetComponent<PlayerAutoMove>().enabled = _autoMode;

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;

        //todo : 버튼 클릭할떄 애니메이션 주기 + 사운드 주기
        // 애니메이션 : 코드로 구현 약간 커졌다가 원래대로..
        // 사운드 : 일레븐랩스에서 버튼 클릭 공용 사운드 만들어서 적용
    }

    public void PlayAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0.0f;
    }

    void Update()
    {
        if (!_isBumping) return;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > BumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        // 2. 누적시간과 애니메이션 커브에 따른 스케일 변경 
        float time = _elapsedTime / BumpDuration;
        float curveValue = _bumpCurve.Evaluate(time);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * BumpScale, curveValue);
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}