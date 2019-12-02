using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.ItemComponents;
using Jrpg.InventorySystem.Items;
using Jrpg.InventorySystem;

namespace Jrpg.PartySystem
{
    public class Party
    {
        private Character activeCharacter;
        private Dictionary<string, Character> members;
        private InventoryManager inventoryManager;
        private int wallet;

        public Party(InventoryManager gameInventoryManager)
        {
            activeCharacter = null;
            members = new Dictionary<string, Character>();
            inventoryManager = gameInventoryManager;
            wallet = 0;
        }

        public Character GetActiveCharacter()
        {
            return activeCharacter;
        }

        public void SetActiveCharacter(string name)
        {
            if (!members.ContainsKey(name))
            {
                return;
            }

            activeCharacter = members[name];
        }

        public void ClearActiveCharacter()
        {
            activeCharacter = null;
        }

        public bool UseItem(ItemName name, Character targetCharacter, Dictionary<string, object> keyParameters = null)
        {
            return inventoryManager.Use(name, targetCharacter, keyParameters);
        }

        public bool EquipItem(ItemName name, BodyPart bodyPart)
        {
            if (UseItem(name, activeCharacter))
            {
                activeCharacter.Body.Set(bodyPart, name);
            }

            return true;
        }

        public bool UnequipItem(BodyPart bodyPart)
        {
            ItemName itemName = activeCharacter.Body.Get(bodyPart);

            if (itemName != null)
            {
                inventoryManager.Restore((ItemName)itemName, activeCharacter);
                activeCharacter.Body.Remove(bodyPart);
                return true;
            }
            return false;
        }

        public void AddMember(string name, Character member)
        {
            members.Add(name, member);

            if (activeCharacter == null)
            {
                activeCharacter = members[name];
            }
        }

        public void RemoveMember(string name)
        {
            var member = members[name];

            members.Remove(name);

            if (activeCharacter == member)
            {
                if (members.Keys.Count == 0)
                {
                    activeCharacter = null;
                }
                else if (members.Keys.Count > 1)
                {
                    activeCharacter = members.Values.GetEnumerator().Current;
                }
            }
        }

        public Character GetMember(string name)
        {
            if(!members.ContainsKey(name))
            {
                throw new KeyNotFoundException();
            }

            return members[name];
        }

        public List<Character> GetMembers()
        {
            return new List<Character>(members.Values);
        }

        public int GetWalletAmount()
        {
            return wallet;
        }

        public List<ItemInfo> QueryAllItems()
        {
            return inventoryManager.QueryAll();
        }

        public ItemInfo QueryFor(ItemName name)
        {
            List<ItemInfo> data = inventoryManager.QueryAll();
            ItemInfo info = data.Find(x => x.Name.ToString().Equals(name.ToString()));

            return info;
        }

        public void ReceiveItem(BaseItem item, ItemReceiveAction action)
        {
            switch (action)
            {
                case ItemReceiveAction.Purchase:
                    wallet -= item.Value;
                    inventoryManager.Acquire(item);
                    break;
                default:
                    inventoryManager.Acquire(item);
                    break;
            }
        }

        public void ReleaseItem(ItemName name, ItemReleaseAction action)
        {
            switch (action)
            {
                case ItemReleaseAction.Sell:
                    var item = QueryFor(name);
                    wallet += item.Value;
                    inventoryManager.Drop(name);
                    break;
                default:
                    inventoryManager.Drop(name);
                    break;
            }
        }
    }
}
