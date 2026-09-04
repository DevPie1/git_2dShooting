using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    public float _speed = 0.5f;

    protected Transform playerTransform;

    protected GameObject Player;

    protected int Damage;

    private EnemyState _enemyState;

    void Awake()
    {
        _enemyState = GetComponent<EnemyState>();
        Damage = 10;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            if (playerState != null)
            {
                playerState.TakeDamage(Damage);
                _enemyState.Die();
            }
        }
    }

    protected abstract void Move();
}