using System;
namespace Jrpg.InventorySystem.PgItems
{
    public class ValueObject
    {
        public string Operation { get; set; }
        public double Value { get; set; }

        public override bool Equals(object obj)
        {
            var that = (ValueObject)obj;

            if(that.Operation != null && Operation != null
                && !that.Operation.Equals(Operation))
            {
                return false;
            }
            if((that.Operation != null && Operation == null) ||
                (that.Operation == null && Operation != null))
            {
                return false;
            }
            if(!that.Value.Equals(Value))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Operation, Value);
        }
    }
}
