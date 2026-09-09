using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;

    public void StartTrail()
    {
        _trail.emitting = true;
    }

    public void StopTrail()
    {
        _trail.emitting = false;
    }
}