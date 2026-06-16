using Interfaces;
using Project.Gameplay.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Gameplay.UI
{
    public class UIItem : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image itemIcon;
        //[SerializeField] private TextMeshProUGUI itemName;
        //[SerializeField] private TextMeshProUGUI descripton;
        [SerializeField] private TextMeshProUGUI itemCount;

        public bool IsReleased { get; private set; } = false;

        public void OnCreate(InventoryItemData data, int value) // eventually setup widgets that display on hover and pass item name / description
        {
            itemIcon.sprite = data.Icon;
            itemCount.text = value.ToString();
        }

        public void UpdateCount(int value)
        {
            itemCount.text = value.ToString();
        }

        public void Release()
        {
            if(IsReleased) return;

            IsReleased = true;

            ObjectPoolManager.Release(gameObject);
        }

        public void OnSpawn()
        {
            IsReleased = false;
        }

        public void OnDespawn()
        {

        }
    }
}