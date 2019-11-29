using Xunit;
using Jrpg.System.Tests.Common;
using Jrpg.InventorySystem;
using Jrpg.InventorySystem.PgItems;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.PartySystem;
using Jrpg.CharacterSystem;
using Jrpg.ItemComponents;

namespace Jrpg.System.Tests
{
    public class TestUseEquipAndUnequipItems
    {

        private InventoryManager inventoryManager;
        private Party party;
        private IDbReader itemsDbReader;
        private TestUtilities testUtility;

        public TestUseEquipAndUnequipItems()
        {
            itemsDbReader = new ItemsDbReader();
            testUtility = new TestUtilities(itemsDbReader.ReadData<Item>(DbConstants.ItemsDbFile));
        }

        [Fact]
        public void TestPotionOnSelf()
        {
            inventoryManager = new InventoryManager();

            testUtility.SeedInventoryManager(inventoryManager);

            Character terra = new Character("Terra");
            party = new Party(inventoryManager);
            party.AddMember(terra.Name, terra);

            // Pretend we're in the menu screen, and have selected Terra's profile.
            party.SetActiveCharacter(terra.Name);

            Assert.Equal(30, terra.Statistics[StatisticType.HpCurrent].CurrentValue);

            // Terra should take damage
            terra.Statistics[StatisticType.HpCurrent].CurrentValue = 0;

            bool useResult;

            ItemInfo i = party.QueryFor(TestUtilities.Tonic);

            useResult = party.UseItem(TestUtilities.Tonic, terra);
            Assert.True(useResult);
            Assert.Equal(10, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(2, party.QueryFor(TestUtilities.Tonic).Quantity);

            useResult = party.UseItem(TestUtilities.Tonic, terra);
            Assert.True(useResult);
            Assert.Equal(20, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(1, party.QueryFor(TestUtilities.Tonic).Quantity);

            useResult =  party.UseItem(TestUtilities.Tonic, terra);
            Assert.True(useResult);
            Assert.Equal(30, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.Tonic).Quantity);

            useResult = party.UseItem(TestUtilities.Tonic, terra);
            Assert.False(useResult);
            Assert.Equal(30, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.Tonic).Quantity);
        }

        [Fact]
        public void TestPotionOnTarget()
        {
            inventoryManager = new InventoryManager();

            testUtility.SeedInventoryManager(inventoryManager);

            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            Character locke = new Character("Locke");

            party.AddMember(terra.Name, terra);
            party.AddMember(locke.Name, locke);

            Assert.Equal(30, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, locke.Statistics[StatisticType.HpCurrent].CurrentValue);

            // Make them lose HP.
            terra.Statistics[StatisticType.HpCurrent].CurrentValue = 0;
            locke.Statistics[StatisticType.HpCurrent].CurrentValue = 0;

            bool useResult;

            // Pretend we're in battle, and Terra is the character in turn.
            party.SetActiveCharacter(terra.Name);

            // Terra uses the Potion on Locke.
            useResult = party.UseItem(TestUtilities.Tonic, locke);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(10, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(2, party.QueryFor(TestUtilities.Tonic).Quantity);


            // Pretend we're in battle, and Terra is the character in turn.
            party.SetActiveCharacter(terra.Name);

            // Terra uses the Potion on Locke.
            useResult = party.UseItem(TestUtilities.Tonic, locke);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(20, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(1, party.QueryFor(TestUtilities.Tonic).Quantity);


            // Pretend we're in battle, and Terra is the character in turn.
            party.SetActiveCharacter(terra.Name);

            // Terra uses the Potion on Locke.
            useResult = party.UseItem(TestUtilities.Tonic, locke);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(30, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.Tonic).Quantity);


            // Pretend we're in battle, and Terra is the character in turn.
            party.SetActiveCharacter(terra.Name);

            // Terra uses the Potion on Locke.
            useResult = party.UseItem(TestUtilities.Tonic, locke);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.False(useResult);
            Assert.Equal(30, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.Tonic).Quantity);
        }


        [Fact]
        public void TestPotionOnSelfAndTarget()
        {
            inventoryManager = new InventoryManager();

            testUtility.SeedInventoryManager(inventoryManager);

            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            Character locke = new Character("Locke");

            party.AddMember(terra.Name, terra);
            party.AddMember(locke.Name, locke);

            Assert.Equal(30, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(30, locke.Statistics[StatisticType.HpCurrent].CurrentValue);

            // Takes damage
            terra.Statistics[StatisticType.HpCurrent].CurrentValue = 0;
            locke.Statistics[StatisticType.HpCurrent].CurrentValue = 0;

            bool useResult;

            // Pretend we're in battle, and Terra is the character in turn.
            party.SetActiveCharacter(terra.Name);

            // Terra chooses to heal Locke
            useResult = party.UseItem(TestUtilities.Tonic, locke);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(10, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(2, party.QueryFor(TestUtilities.Tonic).Quantity);

            // Pretend we're in battle, and Locke is the character in turn.
            party.SetActiveCharacter(locke.Name);

            // Locke uses Potion on Terra
            useResult = party.UseItem(TestUtilities.Tonic, terra);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(10, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(10, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(1, party.QueryFor(TestUtilities.Tonic).Quantity);

            // Pretend we're in battle, and Locke is the character in turn.
            party.SetActiveCharacter(terra.Name);

            // Terra uses potion on Self!
            useResult = party.UseItem(TestUtilities.Tonic, terra);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(20, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(10, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.Tonic).Quantity);


            // Pretend we're in battle, and Locke is the character in turn.
            party.SetActiveCharacter(locke.Name);

            // Locke uses Potion on Terra
            useResult = party.UseItem(TestUtilities.Tonic, terra);

            // Turn ends
            party.ClearActiveCharacter();

            Assert.False(useResult);
            Assert.Equal(20, terra.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(10, locke.Statistics[StatisticType.HpCurrent].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.Tonic).Quantity);
        }

        [Fact]
        public void TestEquip()
        {
            inventoryManager = new InventoryManager();

            testUtility.SeedInventoryManager(inventoryManager);

            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            Character locke = new Character("Locke");

            party.AddMember(terra.Name, terra);
            party.AddMember(locke.Name, locke);

            Assert.Equal(1, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(1, locke.Statistics[StatisticType.Defense].CurrentValue);

            bool useResult;

            party.SetActiveCharacter(terra.Name);
            useResult = party.EquipItem(TestUtilities.LeatherHelmet, BodyPart.Head);
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(1, locke.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(4, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(1, party.QueryFor(TestUtilities.LeatherHelmet).Quantity);

            party.SetActiveCharacter(locke.Name);
            useResult = party.EquipItem(TestUtilities.LeatherHelmet, BodyPart.Head);
            party.ClearActiveCharacter();

            Assert.True(useResult);
            Assert.Equal(4, locke.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(4, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(0, party.QueryFor(TestUtilities.LeatherHelmet).Quantity);

            party.SetActiveCharacter(terra.Name);
            useResult = party.UnequipItem(BodyPart.Head);
            party.ClearActiveCharacter();
            Assert.True(useResult);

            Assert.Equal(4, locke.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(1, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(1, party.QueryFor(TestUtilities.LeatherHelmet).Quantity);

            party.SetActiveCharacter(terra.Name);
            useResult = party.EquipItem(TestUtilities.IronHelmet, BodyPart.Head);
            party.ClearActiveCharacter();
            Assert.True(useResult);

            Assert.Equal(4, locke.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(8, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(1, party.QueryFor(TestUtilities.LeatherHelmet).Quantity);
            Assert.Equal(0, party.QueryFor(TestUtilities.IronHelmet).Quantity);

            party.SetActiveCharacter(locke.Name);
            useResult = party.UnequipItem(BodyPart.Head);
            party.ClearActiveCharacter();
            Assert.True(useResult);

            Assert.Equal(1, locke.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(8, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(2, party.QueryFor(TestUtilities.LeatherHelmet).Quantity);
            Assert.Equal(0, party.QueryFor(TestUtilities.IronHelmet).Quantity);


            party.SetActiveCharacter(terra.Name);
            useResult = party.UnequipItem(BodyPart.Head);
            party.ClearActiveCharacter();
            Assert.True(useResult);

            Assert.Equal(1, locke.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(1, terra.Statistics[StatisticType.Defense].CurrentValue);
            Assert.Equal(2, party.QueryFor(TestUtilities.LeatherHelmet).Quantity);
            Assert.Equal(1, party.QueryFor(TestUtilities.IronHelmet).Quantity);
        }
    }
}
