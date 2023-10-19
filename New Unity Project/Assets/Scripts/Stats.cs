using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Enemy")]
public class Stats : ScriptableObject
{
   public string name;
   public int health;
   public int attack;
   public int speed;
}
