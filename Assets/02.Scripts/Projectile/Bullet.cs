using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction = Vector2.up; //new Vector2(0, 1);

    private float _speed = 3f;

    public int _damage = 20;

    private void Update()
    {
        this.transform.Translate(_direction * _speed * Time.deltaTime);
    }

    // 충동 관련 이벤트 (ENter -> Stay -> Exit)
    //충돌이 시작되면 호출되는 이벤트 함수
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("충돌 했다!");


        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyState _enemy = other.gameObject.GetComponent<EnemyState>();
            if (_enemy != null)
            {
                _enemy.TakeDamage(_damage); // Enemy 스크립트에 있는 데미지 함수 호출 (예시)
            }

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("충돌 중이다");
    }
}