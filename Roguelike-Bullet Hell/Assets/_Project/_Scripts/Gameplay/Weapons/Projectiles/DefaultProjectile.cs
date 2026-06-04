using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : ProjectileBase
{
    private Vector3 velocity;

    protected override void OnInitalize()
    {
        velocity = projectileSpeed * cachedTransform.forward;
    }

    protected override void HandleMovement()
    {
        cachedTransform.position += Time.deltaTime * velocity;
    }
}
