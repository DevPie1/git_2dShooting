using UnityEngine;

public class MoveSpeedItem : Item
{
    [SerializeField] private float _increaseAmount = 1f;

    protected override void ApplyEffect(PlayerState player)
    {
        player.IncreaseMoveSpeed(_increaseAmount);
    }
}