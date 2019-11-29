using System;
using System.Collections.Generic;
using Jrpg.InventorySystem.Utils;

namespace Jrpg.InventorySystem.PgItems
{
    public class ItemGenerator
    {
        private enum AffixType
        {
            Prefix,
            Suffix
        }

        private List<Item> items;
        private List<Affix> prefixes;
        private List<Affix> suffixes;

        public ItemGenerator(List<Item> items, List<Affix> prefixes, List<Affix> suffixes)
        {
            this.items = items;
            this.prefixes = prefixes;
            this.suffixes = suffixes;
        }

        private Item FindBaseItem(ItemClassEdge edge)
        {
            var item = items.Find(i => i.Name.Equals(edge.Name));
            var itemEdges = item.ItemClass;

            if(itemEdges.Count == 0)
            {
                return new Item()
                {
                    Name = item.Name,
                    ItemClass = new List<ItemClassEdge>(item.ItemClass),
                    Properties = new List<Property>(item.Properties),
                    Value = item.Value,
                    BodyPart = item.BodyPart,
                    IsKeyItem = item.IsKeyItem,
                    PublishHandler = item.PublishHandler
                };
            }

            var randomItemEdge = RandomUtil.GetRandomItemEdge(itemEdges);

            return FindBaseItem(randomItemEdge);
        }

        private Affix GetRandomAffix(string initialItemClassName, AffixType affixType)
        {
            var validAffixes = new List<Affix>();

            if(affixType == AffixType.Prefix)
            {
                validAffixes = new List<Affix>(prefixes);
            } else if(affixType == AffixType.Suffix)
            {
                validAffixes = new List<Affix>(suffixes);
            }

            validAffixes.RemoveAll(p => !p.ParentItemClass.Contains(initialItemClassName));

            if(validAffixes.Count == 0)
            {
                return affixType == AffixType.Prefix
                    ? prefixes.Find(x => x.Name.Equals(Affix.AffixLabel.NoPrefix))
                    : suffixes.Find(x => x.Name.Equals(Affix.AffixLabel.NoSuffix));
            }

            return RandomUtil.GetRandomAffix(validAffixes);
        }

        private void ApplyProperties(Item item, Affix prefix, Affix suffix)
        {
            foreach(Property p in prefix.Properties)
            {
                var currItemProperty = item.Properties.Find(prop => prop.Name.Equals(p.Name));
                if (currItemProperty != null)
                {
                    if (p.Operation != null
                        && p.Operation.Equals(Property.OperationLabel.Multiply))
                    {
                        currItemProperty.Value *= p.Value;
                    }
                    else
                    {
                        currItemProperty.Value += p.Value;
                    }

                    currItemProperty.Value = Math.Round(currItemProperty.Value);
                }
                else
                {
                    item.Properties.Add(new Property() {
                        Name = p.Name,
                        Value = p.Value
                    });
                }
            }

            foreach (Property p in suffix.Properties)
            {
                var currItemProperty = item.Properties.Find(prop => prop.Name.Equals(p.Name));
                if (currItemProperty != null)
                {
                    if (p.Operation != null
                        && p.Operation.Equals(Property.OperationLabel.Multiply))
                    {
                        currItemProperty.Value *= p.Value;
                    }
                    else
                    {
                        currItemProperty.Value += p.Value;
                    }

                    currItemProperty.Value = Math.Round(currItemProperty.Value);
                }
                else
                {
                    item.Properties.Add(new Property() {
                        Name = p.Name,
                        Value = p.Value
                    });
                }
            }
        }

        public Item GenerateItem(DropSource source)
        {
            var drops = source.ItemClass;
            var startItemEdge = RandomUtil.GetRandomItemEdge(drops);

            var result = FindBaseItem(startItemEdge);
            if(!result.Name.Equals("NoDrop"))
            {
                var prefix = GetRandomAffix(startItemEdge.Name, AffixType.Prefix);
                var suffix = GetRandomAffix(startItemEdge.Name, AffixType.Suffix);

                if (!prefix.Name.Equals(Affix.AffixLabel.NoPrefix))
                {
                    result.Name = $"{prefix.Name} {result.Name}";
                }
                if (!suffix.Name.Equals(Affix.AffixLabel.NoSuffix))
                {
                    result.Name = $"{result.Name} {suffix.Name}";
                }

                ApplyProperties(result, prefix, suffix);
            }

            return result;
        }

        public Item RetrieveItemByName(string itemDefinitionName)
        {
            var item = items.Find(i => i.Name.Equals(itemDefinitionName));

            if(item == null)
            {
                throw new NullReferenceException(
                	"Item could not be found in the registry to be generated by name."
                );
            }

            return item;
        }
    }
}
