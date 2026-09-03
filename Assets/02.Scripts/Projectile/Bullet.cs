using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 _direction = Vector2.up; //new Vector2(0, 1);

    private float _speed = 3f;

    private void Update()
    {
        this.transform.Translate(_direction * _speed * Time.deltaTime);
    }
}