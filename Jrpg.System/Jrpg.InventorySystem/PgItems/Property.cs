using System;
namespace Jrpg.InventorySystem.PgItems
{
    public class Property
    {
        public static class OperationLabel
        {
            public static string Addition = "Addition";
            public static string Multiply = "Multiply";
        }

        public string Name { get; set; }
        public string Operation { get; set; }
        public double Value { get; set; }

        public override bool Equals(object obj)
        {
            var that = (Property)obj;

            if(!that.Name.Equals(Name))
            {
                return false;
            }
            if(that.Operation != null && Operation != null
                && !that.Operation.Equals(Operation))
            {
                return false;
            }
            if(that.Operation == null && Operation != null
                || that.Operation != null && Operation == null)
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
            return HashCode.Combine(Name, Operation, Value);
        }
    }
}
