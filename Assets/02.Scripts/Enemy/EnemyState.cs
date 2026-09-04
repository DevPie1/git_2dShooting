using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public void Die()
    {
        Destroy(this.gameObject);
    }
}