using System;
using System.Collections.Generic;
using Xunit;
using Jrpg.System.Tests.Common;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.InventorySystem.PgItems;
using Jrpg.InventorySystem;
using Jrpg.InventorySystem.Items;
using Jrpg.CharacterSystem;
using Jrpg.ItemComponents;
using Jrpg.PartySystem;

namespace Jrpg.System.Tests
{
    public class TestGeneratingItems
    {
        private InventoryManager inventoryManager;
        private Party party;

        private List<Monster> monsters;
        private ItemGenerator generator;

        private IDbReader monstersDbReader;
        private IDbReader itemsDbReader;
        private IDbReader prefixesDbReader;
        private IDbReader suffixesDbReader;

        [Fact]
        public void TestGenerateBaseItem()
        {
            monstersDbReader = new DropsDbReader();
            monsters = monstersDbReader.ReadData<Monster>(DbConstants.MonstersDbFile);

            itemsDbReader = new MockedItemsDbReader();
            prefixesDbReader = new MockedNoAffixDbReader();
            suffixesDbReader = new MockedNoAffixDbReader();

            generator = new ItemGenerator(
                itemsDbReader.ReadData<Item>(""),
                prefixesDbReader.ReadData<Affix>("Prefixes"),
                suffixesDbReader.ReadData<Affix>("Suffixes")
            );

            var originalMonstersItemClass = new List<ItemClassEdge>(monsters[0].ItemClass);
            monsters[0].ItemClass.RemoveAll(i => !i.Name.Equals("Sword 1"));

            var item = generator.GenerateItem(monsters[0]);

            var diamondSword = new Item
            {
                Name = "Diamond Sword",
                BodyPart = "Arms",
                ItemClass = new List<ItemClassEdge>(),
                Properties = new List<Property> { new Property { Name = CharacterStatistics.LabelAttack, Value = 100 } },
                Value = 5
            };
            Assert.NotNull(item);
            Assert.Equal(diamondSword, item);

            monsters[0].ItemClass = originalMonstersItemClass;

            inventoryManager = new InventoryManager();
            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            party.AddMember(terra.Name, terra);

            var DiamondSwordName = new ItemName(diamondSword.Name);
            var diamondSwordItem = new BaseItem(diamondSword);
            party.ReceiveItem(diamondSwordItem, ItemReceiveAction.Treasure);

            party.SetActiveCharacter("Terra");
            party.EquipItem(DiamondSwordName, BodyPart.Arms);

            Assert.Equal(101, terra.Statistics[StatisticType.Attack].CurrentValue);
        }

        [Fact]
        public void TestGenerateComplexItem()
        {
            monstersDbReader = new DropsDbReader();
            monsters = monstersDbReader.ReadData<Monster>(DbConstants.MonstersDbFile);

            itemsDbReader = new MockedItemsDbReader();
            prefixesDbReader = new MockedAffixDbReader();
            suffixesDbReader = new MockedAffixDbReader();

            generator = new ItemGenerator(
                itemsDbReader.ReadData<Item>(""),
                prefixesDbReader.ReadData<Affix>("Prefixes"),
                suffixesDbReader.ReadData<Affix>("Suffixes")
            );

            var originalMonstersItemClass = new List<ItemClassEdge>(monsters[0].ItemClass);
            monsters[0].ItemClass.RemoveAll(i => !i.Name.Equals("Sword 1"));

            var item = generator.GenerateItem(monsters[0]);

            var PrismaticDiamondSwordOfTheSunItem = new Item
            {
                Name = "Prismatic Diamond Sword of the Sun",
                BodyPart = "Arms",
                ItemClass = new List<ItemClassEdge>(),
                Properties = new List<Property> {
                    new Property { Name = CharacterStatistics.LabelAttack, Value = 100 },
                    new Property { Name = CharacterStatistics.LabelMagic, Value = 67 },
                    new Property { Name = CharacterStatistics.LabelSpeed, Value = 33 }
                },
                Value = 5
            };

            Assert.Equal(PrismaticDiamondSwordOfTheSunItem, item);

            inventoryManager = new InventoryManager();
            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            party.AddMember(terra.Name, terra);

            party.ReceiveItem(new BaseItem(item), ItemReceiveAction.Loot);

            party.SetActiveCharacter("Terra");
            party.EquipItem(new ItemName("Prismatic Diamond Sword of the Sun"), BodyPart.Arms);

            Assert.Equal(101, terra.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(68, terra.Statistics[StatisticType.Magic].CurrentValue);
            Assert.Equal(34, terra.Statistics[StatisticType.Speed].CurrentValue);

            party.UnequipItem(BodyPart.Arms);
            Assert.Equal(1, terra.Statistics[StatisticType.Attack].CurrentValue);
            Assert.Equal(1, terra.Statistics[StatisticType.Magic].CurrentValue);
            Assert.Equal(1, terra.Statistics[StatisticType.Speed].CurrentValue);
        }
    }
}
