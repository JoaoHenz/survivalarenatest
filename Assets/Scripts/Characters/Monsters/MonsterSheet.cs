using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Monster Sheet", menuName = "Monster Sheet")]
public class MonsterSheet : ScriptableObject {

    public new string name = "New Monster";
    public string description = "";
    public int damage = 10;
    public int health = 10;
    public float speed = 2f;
    public float range = 20.0f;
    public float attackCooldown = 2f;
    public int scoreForKill = 10;
    public enum Color { Teal,Red,Green,Blue,Purple,Yellow};
    public Color color = Color.Red;


}
