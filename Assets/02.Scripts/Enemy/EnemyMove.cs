using System;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    public float _speed = 0.5f;

    protected Transform playerTransform;


    void Update()
    {
        Move();
    }

    protected abstract void Move();
}