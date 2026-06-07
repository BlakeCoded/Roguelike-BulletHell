using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using Project.Gameplay.Pooling;
using TMPro;
using UnityEngine;
using static Helper;

namespace Project.UI
{
    public class DamageTextUI : MonoBehaviour, IPoolable
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float lifeTime;
        [SerializeField] private Color normalColour;
        [SerializeField] private Color critColour;

        public bool IsReleased { get; private set; }

        private float timer;
        private Transform cachedTransform;
        private Vector3 position;

        private void Awake()
        {
            cachedTransform = transform;
        }

        public void Initialize(DamageResult result, Vector3 hitPosition)
        {
            this.position = hitPosition;

            cachedTransform.position = MainCamera.WorldToScreenPoint(this.position);

            text.text = result.DamageDealt.ToString("0");

            if(!result.IsCritical)
            {
                text.color = normalColour;
                return;
            }

            text.color = critColour;
        }

        private void Update()
        {
            position += 2 * GameTime.DeltaTime * Vector3.up;

            cachedTransform.position = MainCamera.WorldToScreenPoint(this.position);

            timer += GameTime.DeltaTime;

            if(timer > lifeTime)
            {
                if(!IsReleased)
                {
                    ObjectPoolManager.Release(gameObject);
                    IsReleased = true;
                }
            }
        }
        public void OnSpawn()
        {
            timer = 0;
            IsReleased = false;
        }

        public void OnDespawn()
        {
            
        }
    }
}