using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : ProjectileBase
{
    protected override void HandleMovement()
    {
        transform.MoveBy(transform.forward * MoveSpeed * Time.deltaTime);
    }
}
