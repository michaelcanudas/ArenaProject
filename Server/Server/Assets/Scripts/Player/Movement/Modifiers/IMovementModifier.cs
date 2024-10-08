using UnityEngine;

public interface IMovementModifier
{
    Vector3 Modify(ModifierInfo info, PlayerMoverConfig config);
}

public class ModifierInfo
{
    public bool IsGrounded { get; set; }
    public Vector3 CurrentHorizontalVelocity { get; set; }
    public Vector3 CurrentVelocity { get; set; }
    public float CurrentMaxMoveSpeed { get; set; }
    public bool IsCrouching { get; set; }
    public Message.PlayerInputMessage Input { get; set; }
}