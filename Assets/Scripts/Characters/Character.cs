using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public enum Team { Player, Monsters };
    public Team team;
    public virtual int HealthPoints { get; set; }
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    protected AudioSource audioSource;



    protected void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Sound Effects Volume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("Sound Effects Volume");
        }

    }
}
