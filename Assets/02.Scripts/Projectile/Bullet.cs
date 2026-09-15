using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private Bullet[] _bulletPrefabs;
    private Vector2 _direction = Vector2.up; //new Vector2(0, 1);

    private float _speed = 3f;

    private AudioSource _audioSource;

    public int damage = 20;

    private int _poolSize = 50;

    private Bullet[] _pool;

    [SerializeField] BulletType _type;
    public BulletType Type => _type;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound()
    {
        _audioSource.pitch = UnityEngine.Random.Range(1f, 3f);
        _audioSource.Play();
    }

    public void OnSpawn()
    {
        PlaySound();
    }

    private void Update()
    {
        this.transform.Translate(_direction * _speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyState _enemy = other.gameObject.GetComponent<EnemyState>();
            if (_enemy != null)
            {
                int finalDamage = damage + (int)UpgradeManager.Instance.Upgrades[0].CurrentValue;
                _enemy.TakeDamage(finalDamage);
            }

            this.gameObject.SetActive(false);
        }
    }
}