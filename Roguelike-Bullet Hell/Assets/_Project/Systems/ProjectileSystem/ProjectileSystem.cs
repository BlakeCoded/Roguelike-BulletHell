using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public static class ProjectileSystem
{
    private static readonly List<IProjectile> projectiles = new();

    private static readonly List<IProjectile> pendingRemoval = new();

    public static void Register(IProjectile projectile)
    {
        projectile.Index = projectiles.Count;
        projectiles.Add(projectile);
    }

    private static void Unregister(IProjectile projectile)
    {
        int index = projectile.Index;
        int lastIndex = projectiles.Count - 1;

        if (index < 0 || index > lastIndex)
        {
            projectiles.RemoveAt(lastIndex);
            return;
        }

        if(index != lastIndex)
        {
            IProjectile last = projectiles[lastIndex];

            projectiles[index] = last;
            last.Index = index;
        }

        projectiles.RemoveAt(lastIndex);
        projectile.Index = -1;
    }

    public static void Tick(float deltaTime)
    {
        for(int i = projectiles.Count -1; i >= 0; i--)
        {
            projectiles[i].Tick(deltaTime);
        }

        for(int i = 0; i < pendingRemoval.Count; i++)
        {
            Unregister(pendingRemoval[i]);
        }

        pendingRemoval.Clear();
    }

    public static void MarkForRemoval(IProjectile projectile)
    {
        pendingRemoval.Add(projectile);
    }
}