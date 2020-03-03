using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GameManager Sheet", menuName = "GameManager Sheet")]
public class GameManagerSheet : ScriptableObject
{

    public new string name = "New GameManager";
    public SpawnCronogram spawnCronogram;
    public int medKitHealingAmount = 50;
    public int ammoKitAmmoAmount = 10;
    [Range(0, 100)]
    public float spawnRateAmmoKit = 50;
    [Range(0, 100)]
    public float spawnRateMedKit = 50;

}
