using System.Collections.Generic;
using Project.Gameplay.Pooling;
using UnityEngine;

namespace Project.Gameplay.UI
{
    public class InventoryPanel : UIPanel
    {
        private CombatEntity owner;
        private readonly Dictionary<InventoryItemData, int> itemDisplay = new();
        private readonly Dictionary<InventoryItemData, UIItem> uiItems  = new();

        [SerializeField] private UIItem UiItemPrefab;
        [SerializeField] private Transform inventoryTransform;

        private bool isDirty { get; set; } = true;

        public void Init(CombatEntity owner)
        {
            this.owner = owner;

            owner.Inventory.OnItemAdded += OnItemAdded;
            owner.Inventory.OnItemRemoved += OnItemRemoved;
        }

        protected override void OnOpen()
        {
            RebuildUiDisaplay();
        }

        protected override void OnClose()
        {
            
        }

        private void RebuildUiDisaplay()
        {
            if (!isDirty) return;

            itemDisplay.Clear();

            foreach(UIItem ui in uiItems.Values)
            {
                ui.Release();
            }

            uiItems.Clear();

            foreach(var item in owner.Inventory.Items)
            {
                if(!itemDisplay.TryGetValue(item.Data, out int value))
                {
                    itemDisplay[item.Data] = 1;
                    continue;
                }

                int count = value + 1;

                itemDisplay[item.Data] = count;
            }

            foreach(InventoryItemData data in itemDisplay.Keys)
            {
                AddUiItem(data);
            }

            isDirty = false;
        }

        private void OnItemAdded(InventoryItem item)
        {
            if(!IsOpen)
            {
                isDirty = true;
                return;
            }

            if (!itemDisplay.TryGetValue(item.Data, out int value))
            {
                itemDisplay[item.Data] = 1;
                AddUiItem(item.Data);
                return;
            }

            int count = value + 1;
            itemDisplay[item.Data] = count;

            UpdateUIItem(item);
        }

        private void OnItemRemoved(InventoryItem item)
        {
            if(!IsOpen)
            {
                isDirty = true;
                return;
            }

            if(itemDisplay.TryGetValue(item.Data, out int value))
            {
                int count = value - 1;

                if(count <= 0)
                {
                    RemoveUiItem(item);
                    return;
                }

                itemDisplay[item.Data] = count;
                UpdateUIItem(item);
            }
        }

        private void UpdateUIItem(InventoryItem item)
        {
            UIItem uIItem = uiItems[item.Data];
            int count = itemDisplay[item.Data];

            uIItem.UpdateCount(count);
        }

        private void AddUiItem(InventoryItemData item)
        {
            if(uiItems.ContainsKey(item)) return;

            int count = itemDisplay[item];

            UIItem ui = ObjectPoolManager.GetUI<UIItem>(UiItemPrefab, inventoryTransform);

            ui.OnCreate(item, count);

            uiItems.Add(item, ui);
        }

        private void RemoveUiItem(InventoryItem item)
        {
            UIItem ui = uiItems[item.Data];

            itemDisplay.Remove(item.Data);
            uiItems.Remove(item.Data);

            ui.Release();
        }

        private void OnDestroy()
        {
            if(owner == null) return;

            owner.Inventory.OnItemAdded -= OnItemAdded;
            owner.Inventory.OnItemRemoved -= OnItemRemoved;
        }
    }
}