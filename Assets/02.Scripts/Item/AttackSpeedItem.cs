using UnityEngine;

public class AttackSpeedItem : Item
{
    [SerializeField] private float _increaseAmount = 0.5f;

    protected override void ApplyEffect(PlayerState player)
    {
        player.IncreaseAttackSpeed(_increaseAmount);
    }
}