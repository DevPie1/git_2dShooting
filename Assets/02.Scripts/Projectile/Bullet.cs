using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction = new Vector2(0, 1);

    private float speed=3f;

    private void Update()
    {
        this.transform.Translate(direction * speed * Time.deltaTime);
    }
}