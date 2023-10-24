using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Stats")]
public class Stats : ScriptableObject
{
   public float health;
   public float attack;
   public float speed = 1;

   public float GetHealth()
   {
      return health;
   }
   public float GetAttack()
   {
      return attack;
   }
   public float GetSpeed()
   {
      return speed;
   }
}
