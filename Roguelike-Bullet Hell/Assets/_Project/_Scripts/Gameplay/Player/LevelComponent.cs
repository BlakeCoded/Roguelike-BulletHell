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

        [Tooltip("Growth Rate of Experience Needed Each Level")]
        [SerializeField] private float growthRate = 1.25f;
        [Tooltip("Starting EXP Needed for First Level")]
        [SerializeField] private int baseExp = 100;

        private void Awake()
        {
            ExpToNextLevel = baseExp;
        }

        public void AddExp(int amount)
        {
            OnExpPickUp?.Invoke(amount);

            CurrentExp += amount;

            while (CurrentExp > ExpToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            OnLevelUp?.Invoke(Level);

            CurrentExp -= ExpToNextLevel;

            Level++;

            ExpToNextLevel = CalculateExpRequired(Level);

            //Debug.Log("Level: " + Level + " Exp to next level: " + ExpToNextLevel);
        }

        private int CalculateExpRequired(int level)
        {
            return Mathf.RoundToInt(baseExp * Mathf.Pow(level, growthRate));
        }
    }
}