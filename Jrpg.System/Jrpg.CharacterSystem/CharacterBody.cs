using System.Collections.Generic;
using Jrpg.ItemComponents;

namespace Jrpg.CharacterSystem
{
    public class CharacterBody
    {
        private Dictionary<BodyPart, ItemName> body;

        public CharacterBody()
        {
            body = new Dictionary<BodyPart, ItemName>();
        }

        public ItemName Get(BodyPart bodyPart)
        {
            if (!body.ContainsKey(bodyPart))
            {
                body.Add(bodyPart, null);
            }

            return body[bodyPart];
        }

        public void Set(BodyPart bodyPart, ItemName itemName)
        {
            body[bodyPart] = itemName;
        }

        public ItemName Remove(BodyPart bodyPart)
        {
            var result = body[bodyPart];

            body[bodyPart] = null;

            return result;
        }
    }
}
