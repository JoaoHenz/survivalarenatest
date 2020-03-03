using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoKit : Kit {

    protected override void GrabKit()
    {
        GameManager.player.Ammo += GameManager.sheet.ammoKitAmmoAmount;
        GameManager.itemAudioSource.clip = sound;
        GameManager.itemAudioSource.Play();
        gameObject.SetActive(false);
    }

}
