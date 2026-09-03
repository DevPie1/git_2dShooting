using System;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    public float _speed = 0.5f;

    protected Transform playerTransform;

    protected virtual void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        Move();
    }

    protected abstract void Move();
}