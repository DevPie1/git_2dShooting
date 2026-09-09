using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    [FormerlySerializedAs("_damage")] public int damage = 99999; // 한방에 죽이는 데미지
    private float _activeDuration = 3f; // 3초 유지
    private float _curSec = 0f;

    private void Update()
    {
        // 코루틴 없이 타이머로 3초 후 자동 파괴
        _curSec += Time.deltaTime;
        if (_curSec >= _activeDuration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bomb Hit");

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyState _enemy = other.gameObject.GetComponent<EnemyState>();
            if (_enemy != null)
            {
                _enemy.TakeDamage(damage);
            }
        }
    }
}