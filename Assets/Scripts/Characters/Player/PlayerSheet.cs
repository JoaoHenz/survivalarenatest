using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player Sheet", menuName = "Player Sheet")]
public class PlayerSheet : ScriptableObject
{

    public new string name = "New Player";
    public string description = "";
    public int damage = 10;
    public int health = 10;
    public float speed = 2f;
    public float attackCooldown = 2f;
    public int startingAmmo = 3;


}
