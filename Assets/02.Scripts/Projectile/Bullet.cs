using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction = Vector2.up; //new Vector2(0, 1);

    private float _speed = 3f;

    private AudioSource _audioSource;

    public int damage = 20;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = UnityEngine.Random.Range(-1.5f, 1.5f);
        _audioSource.Play();
    }

    private void Update()
    {
        this.transform.Translate(_direction * _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bullet Hit");

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyState _enemy = other.gameObject.GetComponent<EnemyState>();
            if (_enemy != null)
            {
                _enemy.TakeDamage(damage);
            }

            Destroy(this.gameObject);
        }
    }
}