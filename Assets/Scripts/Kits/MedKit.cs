using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Kit {

    protected override void GrabKit()
    {
        GameManager.player.HealthPoints += GameManager.sheet.medKitHealingAmount;
        GameManager.itemAudioSource.clip = sound;
        GameManager.itemAudioSource.Play();
        gameObject.SetActive(false);
    }

}
