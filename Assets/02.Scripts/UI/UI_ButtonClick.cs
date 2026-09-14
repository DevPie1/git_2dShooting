using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    private Button _button;

    private Image _myImage;
    private AudioSource _audioSource;

    private bool _autoMode = false;

    [SerializeField] private AnimationCurve _bumpCurve;

    private float _scale = 1.0f;
    private bool _isBumping = false;
    private float _elapsedTime = 0.0f;
    private const float BumpDuration = 0.3f;
    private const float BumpScale = 1.1f;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayAnimation);
        _button.onClick.AddListener(PlaySound);
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

    public void PlayAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0.0f;
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}