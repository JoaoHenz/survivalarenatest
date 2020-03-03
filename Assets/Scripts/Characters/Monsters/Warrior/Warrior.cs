using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Monster {

    public GameObject sword;
    Collider swordCollider;


    new void Start()
    {

        base.Start();
        swordCollider = sword.GetComponent<Collider>() as Collider;
        MeleeWeapon meleeWeapon = sword.GetComponent<MeleeWeapon>() as MeleeWeapon;
        meleeWeapon.damage = monsterSheet.damage;
        meleeWeapon.team = Character.Team.Monsters;
    }

	public void EnableAttackCollider(bool enable)
    {
        swordCollider.enabled = enable;
    }

    protected override void ChangeColor()
    {
        Material armorColor = Resources.Load<Material>("Standard/Materials/warrior/warrior_skeleton_"+ monsterSheet.color.ToString().ToLower());
        if (armorColor)
        {
            Renderer armorRenderer = transform.Find("armor").gameObject.GetComponent<Renderer>();
            Material[] mats = new Material[] { armorColor };
            armorRenderer.materials = mats;
        }
    }

}
