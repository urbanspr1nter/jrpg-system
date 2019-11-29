using System.Collections.Generic;
using System;
using Jrpg.InventorySystem.PgItems;

namespace Jrpg.InventorySystem.Utils
{
    public class RandomUtil
    {
        private static int GetSumOfItemClassEdges(List<ItemClassEdge> edges)
        {
            var sum = 0;
            edges.ForEach(edge => sum += edge.Weight);
            return sum;
        }

        private static int GetSumOfAffixEdges(List<Affix> edges)
        {
            var sum = 0;
            edges.ForEach(edge => sum += edge.Weight);
            return sum;
        }

        private static int GetRandomNumber(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }

        public static ItemClassEdge GetRandomItemEdge(List<ItemClassEdge> edges)
        {
            int S = GetSumOfItemClassEdges(edges);
            int R = GetRandomNumber(1, S);

            int P = 0;
            foreach(ItemClassEdge edge in edges)
            {
                P += edge.Weight;

                if (P >= R)
                {
                    return edge;
                }
            }

            return edges[edges.Count - 1];
        }

        public static Affix GetRandomAffix(List<Affix> affixes)
        {
            int S = GetSumOfAffixEdges(affixes);
            int R = GetRandomNumber(1, S);

            int P = 0;
            foreach(Affix affix in affixes)
            {
                P += affix.Weight;

                if (P >= R)
                {
                    return affix;
                }
            }
            return affixes[affixes.Count - 1];
        }
    }
}
