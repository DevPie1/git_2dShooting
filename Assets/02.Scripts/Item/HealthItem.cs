using UnityEngine;

public class HealthItem : Item
{
    [SerializeField] private int _increaseAmount = 20;

    protected override void ApplyEffect(PlayerState player)
    {
        player.IncreaseHealth(_increaseAmount);
    }
}