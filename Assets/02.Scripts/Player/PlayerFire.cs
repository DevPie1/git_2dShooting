using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //목표 : 스페이스바를 누를때마다 총알을 생성해서 발사하고 싶다.
    //필요속성
    // - 총알 프리팹
    public GameObject _bulletPrefab;
    public GameObject _subBulletPrefab;


    public Transform[] _mainFirePoint;
    public Transform[] _subFirePoint;

    private const float BaseCoolTime = 0.6f;
    private float _coolTimeSec = 0.6f;

    private float _curSec = 0f;

    private bool _isAutoFire = false;
    [SerializeField] private PlayerState _playerState;

    [Header("=== 필살기 (Ultimate) 설정 ===")] public GameObject _bombPrefab; // 생성할 폭탄 프리팹
    private const float BombCoolTimeSec = 10.0f; // 쿨타임 10초
    private float _bombCurSec = 10.0f;

    private void ChangeToAtkSpeed()
    {
        _coolTimeSec = BaseCoolTime / _playerState.AttackSpeed;
    }

    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
        ChangeToAtkSpeed();
    }

    private void Update()
    {
        ChangeToAtkSpeed();

        Fire();
        AutoFire();
        BombFire(); // B키 필살기
    }


    private void AutoFire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isAutoFire = !_isAutoFire;
        }

        if (_isAutoFire)
        {
            if (_curSec >= _coolTimeSec)
            {
                foreach (Transform firePoint in _mainFirePoint)
                {
                    GameObject bullet = Instantiate(_bulletPrefab);
                    bullet.transform.position = firePoint.position;
                }

                foreach (Transform firePoint in _subFirePoint)
                {
                    GameObject bullet = Instantiate(_subBulletPrefab);
                    bullet.transform.position = firePoint.position;
                }

                _curSec = 0f;
            }
        }
    }

    private void Fire()
    {
        _curSec += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_curSec >= _coolTimeSec)
            {
                foreach (Transform firePoint in _mainFirePoint)
                {
                    GameObject bullet = Instantiate(_bulletPrefab);
                    bullet.transform.position = firePoint.position;
                }

                foreach (Transform firePoint in _subFirePoint)
                {
                    GameObject bullet = Instantiate(_subBulletPrefab);
                    bullet.transform.position = firePoint.position;
                }

                _curSec = 0f;
            }
        }
    }


    private void BombFire()
    {
        _bombCurSec += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_bombCurSec >= BombCoolTimeSec)
            {
                if (_bombPrefab != null)
                {
                    // 플레이어 현재 위치에 폭탄 프리팹 생성
                    GameObject bomb = Instantiate(_bombPrefab);
                    bomb.transform.position = this.transform.position;
                }

                _bombCurSec = 0f; // 쿨타임 초기화
            }
        }
    }
}