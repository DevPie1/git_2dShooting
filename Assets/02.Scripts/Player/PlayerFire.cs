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

    private float _coolTimeSec = 0.6f;

    private float _curSec = 0f;

    private bool _isAutoFire = false;

    private void Update()
    {
        Fire();
        AutoFire();
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
}