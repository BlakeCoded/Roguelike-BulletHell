using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Movement;
using UnityEngine;

public class PlayerMovement : MovementComponentBase
{
    [SerializeField] private Transform playerBody;

    public override void Move(Vector3 direction)
    {
        if (!CanMove) return;

        playerBody.position += MoveSpeed * GameTime.DeltaTime * new Vector3(direction.x, 0, direction.y);
    }

    public override void TickMovement() { }
}
