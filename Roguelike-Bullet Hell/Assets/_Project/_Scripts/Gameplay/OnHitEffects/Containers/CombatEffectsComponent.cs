using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using UnityEngine;

namespace OnHitEffect
{
    public class CombatEffectsComponent : MonoBehaviour
    {
        private OnHitEffectContainer onhitEffects = new();

        // Eventually have onkilleffects, oncrit, etc

        public void AddOnHitEffect(IOnHitEffect effect)
        {
            onhitEffects.Add(effect);
        }

        public void RemoveOnHitEffect(IOnHitEffect effect)
        {
            onhitEffects.Remove(effect);
        }

        public void RemoveAllOnHitEffects(object source)
        {
            onhitEffects.RemoveAllFromSource(source);
        }

        public List<OnHitEffectEntry> CreateOnHitEffectSnapshot()
        {
            return onhitEffects.CreateSnapshot();
        }
    }
}