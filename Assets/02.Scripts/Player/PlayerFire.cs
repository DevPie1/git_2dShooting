using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //목표 : 스페이스바를 누를때마다 총알을 생성해서 발사하고 싶다.
    //필요속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    public Transform[] mainFirePoint;
    public Transform[] subFirePoint;

    private float coolTimeSec = 0.6f;

    private float curSec = 0f;
    
    private bool isAutoFire = false;
    private void Update()
    {
        Fire();
        AutoFire();
    }

    private void AutoFire()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isAutoFire = !isAutoFire;
        }

        if (isAutoFire)
        {
            if( curSec >= coolTimeSec)
            {
                foreach (Transform firePoint in mainFirePoint)
                {
                    GameObject bullet = Instantiate(BulletPrefab);
                    bullet.transform.position = firePoint.position;
                    
                }

                foreach (Transform firePoint in subFirePoint)
                {
                    GameObject bullet = Instantiate(SubBulletPrefab);
                    bullet.transform.position = firePoint.position;
                }

                curSec = 0f;
            }
        }
    }

    private void Fire()
    {
        curSec += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if( curSec >= coolTimeSec)
            {
                foreach (Transform firePoint in mainFirePoint)
                {
                    GameObject bullet = Instantiate(BulletPrefab);
                    bullet.transform.position = firePoint.position;
                    
                }

                foreach (Transform firePoint in subFirePoint)
                {
                    GameObject bullet = Instantiate(SubBulletPrefab);
                    bullet.transform.position = firePoint.position;
                }

                curSec = 0f;
            }
            
        }
    }
}