using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction = Vector2.up; //new Vector2(0, 1);

    private float _speed = 3f;

    [FormerlySerializedAs("_damage")] public int damage = 20;

    private void Update()
    {
        this.transform.Translate(_direction * _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌 했다!");


        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyState _enemy = other.gameObject.GetComponent<EnemyState>();
            if (_enemy != null)
            {
                _enemy.Die();
            }

            Destroy(this.gameObject);
        }
    }
}