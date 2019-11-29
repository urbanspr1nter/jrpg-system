using Xunit;
using Jrpg.System.Tests.Common;
using Jrpg.InventorySystem.PgItems;
using Jrpg.InventorySystem;
using Jrpg.InventorySystem.Utils.DbReader;
using Jrpg.CharacterSystem;
using Jrpg.PartySystem;
using Jrpg.ItemComponents;

namespace Jrpg.System.Tests
{
    public class TestReceiveAndReleaseItems
    {
        private InventoryManager inventoryManager;
        private Party party;
        private IDbReader itemsDbReader;
        private TestUtilities testUtility;

        public TestReceiveAndReleaseItems()
        {
            itemsDbReader = new ItemsDbReader();
            testUtility = new TestUtilities(itemsDbReader.ReadData<Item>(DbConstants.ItemsDbFile));
        }

        [Fact]
        public void TestSellingAndPurchaseItem()
        {
            inventoryManager = new InventoryManager();

            testUtility.SeedInventoryManager(inventoryManager);

            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            Character locke = new Character("Locke");

            party.AddMember(terra.Name, terra);
            party.AddMember(locke.Name, locke);

            Assert.Equal(0, party.GetWalletAmount());

            ItemInfo potionInfo;

            party.ReleaseItem(TestUtilities.Tonic, ItemReleaseAction.Sell);

            potionInfo = party.QueryFor(TestUtilities.Tonic);
            Assert.Equal(3, potionInfo.Value);
            Assert.Equal(potionInfo.Value, party.GetWalletAmount());
            Assert.Equal(2, potionInfo.Quantity);

            party.ReceiveItem(testUtility.TonicItem(), ItemReceiveAction.Purchase);

            potionInfo = party.QueryFor(TestUtilities.Tonic);
            Assert.Equal(0, party.GetWalletAmount());
            Assert.Equal(3, potionInfo.Quantity);
        }

        [Fact]
        public void TestAcquireItem()
        {
            inventoryManager = new InventoryManager();
            party = new Party(inventoryManager);

            testUtility.SeedInventoryManager(inventoryManager);

            Character terra = new Character("Terra");
            Character locke = new Character("Locke");

            party.AddMember(terra.Name, terra);
            party.AddMember(locke.Name, locke);

            ItemInfo ironHelmetInfo;

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(1, ironHelmetInfo.Quantity);

            party.ReceiveItem(testUtility.IronHelmetItem(), ItemReceiveAction.Loot);

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(2, ironHelmetInfo.Quantity);

            party.ReceiveItem(testUtility.IronHelmetItem(), ItemReceiveAction.Treasure);

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(3, ironHelmetInfo.Quantity);

            party.ReceiveItem(testUtility.IronHelmetItem(), ItemReceiveAction.Reward);

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(4, ironHelmetInfo.Quantity);
        }

        [Fact]
        public void TestDropItem()
        {
            inventoryManager = new InventoryManager();

            testUtility.SeedInventoryManager(inventoryManager);

            party = new Party(inventoryManager);

            Character terra = new Character("Terra");
            Character locke = new Character("Locke");

            party.AddMember(terra.Name, terra);
            party.AddMember(locke.Name, locke);

            ItemInfo ironHelmetInfo;

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(1, ironHelmetInfo.Quantity);

            party.ReceiveItem(testUtility.IronHelmetItem(), ItemReceiveAction.Loot);
            party.ReceiveItem(testUtility.IronHelmetItem(), ItemReceiveAction.Loot);
            party.ReceiveItem(testUtility.IronHelmetItem(), ItemReceiveAction.Loot);

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(4, ironHelmetInfo.Quantity);

            party.ReleaseItem(TestUtilities.IronHelmet, ItemReleaseAction.Drop);
            party.ReleaseItem(TestUtilities.IronHelmet, ItemReleaseAction.Drop);

            ironHelmetInfo = party.QueryFor(TestUtilities.IronHelmet);
            Assert.Equal(2, ironHelmetInfo.Quantity);
        }
    }
}
