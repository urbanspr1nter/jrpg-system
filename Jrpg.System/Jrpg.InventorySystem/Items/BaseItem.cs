using System;
using System.Collections.Generic;
using Jrpg.CharacterSystem;
using Jrpg.InventorySystem.PgItems;
using Jrpg.ItemComponents;

namespace Jrpg.InventorySystem.Items
{
    public class BaseItem : IItem
    {
        private IPublishHandler publishHandler;
        private List<IItemSubscriber> subscribers;

        public bool IsKeyItem
        {
            get
            {
                return ItemClassDefinition.IsKeyItem;
            }
        }
        public string PublishHandlerName
        {
            get
            {
                return ItemClassDefinition.PublishHandler;
            }
        }
        public int Value
        {
            get
            {
                return Convert.ToInt32(ItemClassDefinition.Value);
            }
        }

        public string Name
        {
            get
            {
                return ItemClassDefinition.Name;
            }
        }

        public BodyPart BodyPart
        {
            get {

                return ToBodyPart(ItemClassDefinition.BodyPart);
            }
        }

        public Item ItemClassDefinition { get; }

        public BaseItem(Item baseItemDef)
        {
            ItemClassDefinition = baseItemDef;

            subscribers = new List<IItemSubscriber>();

            // Generate the publish handler if able
            if (IsKeyItem && PublishHandlerName != null)
            {
                publishHandler = (IPublishHandler)Activator.CreateInstance(
                    Type.GetType(PublishHandlerName),
                    new object[] { }
                );
            }

        }

        private BodyPart ToBodyPart(string bodyPartName)
        {
            if(bodyPartName.Equals("Default"))
            {
                return BodyPart.Default;
            } else if(bodyPartName.Equals("Arms"))
            {
                return BodyPart.Arms;
            } else if(bodyPartName.Equals("Head"))
            {
                return BodyPart.Head;
            }

            return BodyPart.Default;
        }

        private void ApplyCharacterProperty(Character targetChar, Property statProp)
        {
            var value = Convert.ToInt32(statProp.Value);
            var operation = statProp.Operation;

            if(operation == null)
            {
                operation = Property.OperationLabel.Addition;
            }

            if (statProp.Name.Equals(CharacterStatistics.LabelHpCurrent))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.HpCurrent].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.HpCurrent].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelHpMax))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.HpMax].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.HpMax].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMpMax))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MpMax].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MpMax].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMpCurrent))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MpCurrent].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MpCurrent].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelStrength))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Strength].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Strength].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelSpeed))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Speed].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Speed].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelStamina))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Stamina].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Stamina].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMagic))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Magic].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Magic].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelAttack))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Attack].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Attack].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelDefense))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Defense].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Defense].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelEvasion))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Evasion].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Evasion].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMagicDefense))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MagicDefense].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MagicDefense].CurrentValue += value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMagicEvasion))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MagicEvasion].CurrentValue *= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MagicEvasion].CurrentValue += value;
                }
            }
        }

        private void UndoApplyCharacterProperty(Character targetChar, Property statProp)
        {
            var value = Convert.ToInt32(statProp.Value);
            var operation = statProp.Operation;

            if (operation == null)
            {
                operation = Property.OperationLabel.Addition;
            }

            if (statProp.Name.Equals(CharacterStatistics.LabelHpCurrent))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.HpCurrent].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.HpCurrent].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelHpMax))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.HpMax].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.HpMax].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMpCurrent))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MpCurrent].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MpCurrent].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMpMax))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MpMax].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MpMax].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelStrength))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Strength].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Strength].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelSpeed))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Speed].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Speed].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelStamina))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Stamina].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Stamina].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMagic))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Magic].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Magic].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelAttack))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Attack].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Attack].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelDefense))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Defense].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Defense].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelEvasion))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.Evasion].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.Evasion].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMagicDefense))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MagicDefense].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MagicDefense].CurrentValue -= value;
                }
            }
            else if (statProp.Name.Equals(CharacterStatistics.LabelMagicEvasion))
            {
                if (operation.Equals(Property.OperationLabel.Multiply))
                {
                    targetChar.Statistics[StatisticType.MagicEvasion].CurrentValue /= value;
                }
                else
                {
                    targetChar.Statistics[StatisticType.MagicEvasion].CurrentValue -= value;
                }
            }
        }

        public void Register(IItemSubscriber subscriber)
        {
            var existing = subscribers.Find(s => s == subscriber);

            if(existing == null)
            {
                subscribers.Add(subscriber);
            }
        }

        public void Unregister(IItemSubscriber subscriber)
        {
            var existing = subscribers.Find(s => s == subscriber);

            if(existing != null)
            {
                subscribers.Remove(subscriber);
            }
        }

        public bool Apply(Character targetChar, Dictionary<string, object> keyItemParameters = null)
        {
            ItemClassDefinition.Properties
                .ForEach(p => ApplyCharacterProperty(targetChar, p));

            if(IsKeyItem)
            {
                var parameters = keyItemParameters;
                if(parameters == null)
                {
                    parameters = new Dictionary<string, object>();
                }
                var message = publishHandler.GetMessage(parameters);
                foreach (var subscriber in subscribers)
                {
                    subscriber.Publish(message);
                }
            }

            return true;
        }

        public bool CanApply(Character targetChar)
        {
            return true;
        }

        public bool UndoApply(Character targetChar)
        {
            ItemClassDefinition.Properties
                .ForEach(p => UndoApplyCharacterProperty(targetChar, p));

            return true;
        }
    }
}
