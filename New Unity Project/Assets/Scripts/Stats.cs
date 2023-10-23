using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Stats")]
public class Stats : ScriptableObject
{
   public int health;
   public int attack;
   public int speed;

   public int GetHealth()
   {
      return health;
   }
   public int GetAttack()
   {
      return attack;
   }
   public int GetSpeed()
   {
      return speed;
   }
}
