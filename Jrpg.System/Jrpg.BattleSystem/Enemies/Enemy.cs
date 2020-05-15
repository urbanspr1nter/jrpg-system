using System;
using Jrpg.InventorySystem.PgItems;
using Jrpg.CharacterSystem;
using Jrpg.CharacterSystem.Classes;

namespace Jrpg.BattleSystem.Enemies
{
    public class Enemy : Character
    {
        public DropSource dropSource;
        public ItemGenerator itemGenerator;

        public Enemy(string name) : base(name)
        {
            throw new NotSupportedException(
                "Constructor without the EnemyClass provided is not supported."
            );
        }

        public Enemy(string name, BaseCharacterClass defaultDiscipline)
            : base(name, defaultDiscipline)
        {
            throw new NotSupportedException(
                "Constructor without the ItemGenerator provided is not supported."
            );
        }

        public Enemy(
            string name,
            BaseCharacterClass defaultDiscipline,
            ItemGenerator itemGenerator
            ) : base(name, defaultDiscipline)
        {
            if(defaultDiscipline.GetType().BaseType
                != Type.GetType("Jrpg.BattleSystem.Enemies.EnemyClass"))
            {
                throw new NotSupportedException(
                    "Expected discipline type to be of EnemyClass."
                );
            }

            dropSource = new DropSource()
            {
                Name = Name,
                Level = Statistics[StatisticType.Level].CurrentValue,
                ItemClass = ((EnemyClass)(defaultDiscipline)).ItemClasses
            };

            this.itemGenerator = itemGenerator;
        }

        public override bool AddExperience(int experience)
        {
            throw new
                NotImplementedException("Not implemented for Enemy");
        }

        public override void ChangeClass(BaseCharacterClass jobClass)
        {
            throw new
                NotImplementedException("Cannot change the class assigned to the Enemy.");
        }

        public int Gold()
        {
            return ((EnemyClass)currentClass).Gold;
        }

        public int Experience()
        {
            return ((EnemyClass)currentClass).Experience;
        }

        public Item GetItem()
        {
            return itemGenerator.GenerateItem(dropSource);
        }
    }
}
