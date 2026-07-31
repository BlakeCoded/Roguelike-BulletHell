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
    public class DamageTextUI : MonoBehaviour, IPoolable, ITickable
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float lifeTime;
        [SerializeField] private Color normalColour;
        [SerializeField] private Color critColour;

        public int Index { get; private set; }
        public bool IsReleased { get; private set; }

        private float timer;
        private Transform cachedTransform;
        private Vector3 position;

        private float maxSize = 1.2f;
        private float minSize = 0.4f;
        private float maxDistance = 75f;
        private float distance;

        private void Awake()
        {
            cachedTransform = transform;
        }

        public void Initialize(DamageResult result, Vector3 hitPosition)
        {
            this.position = hitPosition;

            cachedTransform.position = MainCamera.WorldToScreenPoint(this.position);

            text.text = result.DamageDealt.ToString("0");

            ScaleTextSizeFromDistance();

            if (!result.IsCritical)
            {
                text.color = normalColour;
            }
            else
            {
                text.color = critColour;
            }
        }

        public void Tick(float deltaTime)
        {
            this.position += 2 * GameTime.DeltaTime * Vector3.up;

            ScaleTextSizeFromDistance();

            Vector3 screenPos = MainCamera.WorldToScreenPoint(this.position);

            bool visible =
                screenPos.x >= 0f && screenPos.x <= Screen.width &&
                screenPos.y >= 0f && screenPos.y <= Screen.height &&
                screenPos.z > 0f;

            text.alpha = visible ? 1f : 0f;

            if (visible)
            {
                cachedTransform.position = screenPos;
            }

            timer += GameTime.DeltaTime;

            if (timer >= lifeTime)
            {
                if (!IsReleased)
                {
                    ObjectPoolManager.Release(gameObject);
                    IsReleased = true;
                }
            }
        }

        public void SetIndex(int index)
        {
            Index = index;
        }

        private void ScaleTextSizeFromDistance()
        {
            distance = Vector3.Distance(MainCamera.transform.position, position);

            float t = Mathf.Clamp01(distance / maxDistance);

            float scale = Mathf.Lerp(maxSize, minSize, t);

            transform.localScale = scale * Vector3.one;
        }

        public void OnSpawn()
        {
            timer = 0;
            IsReleased = false;
        }

        public void OnDespawn()
        {
            GameTextManager.MarkForRemoval(this);
        }
    }
}