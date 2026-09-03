using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector2 _direction = new Vector2(0, -1);

    public float _speed = 0.5f;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}