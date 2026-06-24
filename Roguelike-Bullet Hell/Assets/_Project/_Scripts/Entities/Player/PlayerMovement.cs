using Project.Gameplay.Movement;
using Project.Gameplay.Stats;
using UnityEngine;

public class PlayerMovement : MovementComponentBase
{
    [SerializeField] private Transform playerBody;
    public override bool CanMove { get; set; } = true;
    public override float MoveSpeed => Stats.GetStatValue(StatType.MoveSpeed);

    public override void Move(Vector3 direction)
    {
        if (!CanMove) return;

        Vector3 moveDirection = playerBody.forward * direction.y + playerBody.right * direction.x;

        playerBody.position += MoveSpeed * GameTime.DeltaTime * moveDirection.normalized;
    }

    public override void TickMovement() { }
}