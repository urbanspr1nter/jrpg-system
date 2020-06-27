using Jrpg.BattleSystem.Enemies;
using UnityEngine;

namespace Jrpg.Unity
{
    public class BattleHelper
    {
        public static bool IsWithinSightOf(Enemy enemyCharacter, GameObject enemy, GameObject player)
        {
            var enemyRigidbody2D = enemy.GetComponent<Rigidbody2D>();
            var playerRigidbody2D = player.GetComponent<Rigidbody2D>();

            var distance = VectorUtil.Distance(enemyRigidbody2D.position, playerRigidbody2D.position);
            if (distance > enemyCharacter.Proximity.Sight)
                return false;

            return true;
        }

        public static bool IsWithinAttackOf(Enemy enemyCharacter, GameObject enemy, GameObject player)
        {
            var enemyRigidbody2D = enemy.GetComponent<Rigidbody2D>();
            var playerRigidbody2D = player.GetComponent<Rigidbody2D>();

            var distance = VectorUtil.Distance(enemyRigidbody2D.position, playerRigidbody2D.position);
            if (distance > enemyCharacter.Proximity.Attack)
                return false;

            return true;
        }

        public static bool IsAtMinimumDistanceFrom(Enemy enemyCharacter, GameObject enemy, GameObject player)
        {
            var enemyRigidbody2D = enemy.GetComponent<Rigidbody2D>();
            var playerRigidbody2D = player.GetComponent<Rigidbody2D>();

            var distance = VectorUtil.Distance(enemyRigidbody2D.position, playerRigidbody2D.position);
            if (distance > enemyCharacter.Proximity.KeepAway)
                return false;

            return true;
        }
    }
}
