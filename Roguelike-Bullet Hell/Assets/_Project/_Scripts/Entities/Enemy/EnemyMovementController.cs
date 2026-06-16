using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Movement;
using Project.Gameplay.Stats;
using UnityEngine;

public class EnemyMovementController : MovementComponentBase
{
    [SerializeField] Transform body;

    public override bool CanMove { get; set; }

    public override float MoveSpeed => Stats.GetStatValue(StatType.MoveSpeed);

    float timer;

    public override void TickMovement()
    {
        //timer += GameTime.DeltaTime * MoveSpeed;

        //float z = Mathf.Lerp(0f, 15f, Mathf.PingPong(timer, 1f));

        //body.position = new Vector3(
        //    z,
        //    body.position.y,
        //    body.position.z
        //);
    }

    private void Update()
    {
        TickMovement();
    }
}
