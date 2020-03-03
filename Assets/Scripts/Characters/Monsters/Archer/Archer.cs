using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Monster
{
    public bool insideStrategicPoint=false;
    GameObject[] strategicPoints;
    public float maxDistToSP = 5f;



    public void ShootArrow()
    {
        GameObject arrow = PoolManager.GetObject("Arrow");
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;
        arrow.SetActive(true);
        arrow.GetComponent<Arrow>().AfterEnable(Character.Team.Monsters,monsterSheet.damage);
    }

    public Vector3 FindStrategicPoint()
    {
        strategicPoints = GameManager.strategicPoints;
        GameObject closest = strategicPoints[0];
        foreach (GameObject strategicPoint in strategicPoints)
        {
            if (Vector3.Distance(strategicPoint.transform.position, GameManager.player.transform.position) < Vector3.Distance(closest.transform.position, GameManager.player.transform.position))
            {
                closest = strategicPoint;
            }
        }

        return closest.transform.position;
    }

    protected override void ChangeColor()
    {

        Material armorColor = Resources.Load<Material>("Standard/Materials/archer/archer_armor_" + monsterSheet.color.ToString().ToLower());
        if (armorColor)
        {
            Renderer armorRenderer = transform.Find("armor").gameObject.GetComponent<Renderer>();
            Material[] mats = new Material[] { armorColor };
            armorRenderer.materials = mats;
        }
    }

}
