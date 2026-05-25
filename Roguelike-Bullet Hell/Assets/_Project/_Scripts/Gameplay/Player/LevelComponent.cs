using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    public class LevelComponent : MonoBehaviour
    {
        public int Level { get; private set; } = 1;
        public int CurrentExp { get; private set; }
        public int ExpToNextLevel { get; private set; }

        public Action<int> OnLevelUp;
        public Action<int> OnExpPickUp;

        [SerializeField] private float growthRate = 1.25f;
        [SerializeField] private int baseExp = 100;

        private void Awake()
        {
            ExpToNextLevel = baseExp;
        }

        public void AddExp(int amount)
        {
            CurrentExp += amount;

            while (CurrentExp > ExpToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            CurrentExp -= ExpToNextLevel;

            Level++;

            ExpToNextLevel = CalculateExpRequired(Level);
        }

        private int CalculateExpRequired(int level)
        {
            return Mathf.RoundToInt(baseExp * Mathf.Pow(level, growthRate));
        }
    }
}