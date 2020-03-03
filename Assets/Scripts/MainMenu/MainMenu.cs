using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    AudioSource BGMAudioSource;

    public Slider musicVolume;
    public Slider soundEffectsVolume;
    public Slider initialCountdownDuration;
    public Slider matchDuration;

    public Text ICDText;
    public Text MDText;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        BGMAudioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            BGMAudioSource.volume = PlayerPrefs.GetFloat("Music Volume");
        }
    }

	public void Play()
    {
        SceneManager.LoadScene("cemetery");
    }

    public void Options()
    {
        mainMenuPanel.SetActive(false);


        if (PlayerPrefs.HasKey("Music Volume"))
            musicVolume.value = PlayerPrefs.GetFloat("Music Volume");
        if (PlayerPrefs.HasKey("Sound Effects Volume"))
            soundEffectsVolume.value = PlayerPrefs.GetFloat("Sound Effects Volume");
        if (PlayerPrefs.HasKey("Initial Countdown Duration"))
            initialCountdownDuration.value = PlayerPrefs.GetFloat("Initial Countdown Duration");
        if (PlayerPrefs.HasKey("Match Duration"))
            matchDuration.value = PlayerPrefs.GetFloat("Match Duration");

        ICDText.text = ((int)(initialCountdownDuration.value)).ToString() + " seconds";
        MDText.text = ((int)(matchDuration.value)).ToString() + " seconds";



        optionsPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ConfirmChanges()
    {
        optionsPanel.SetActive(false);


        PlayerPrefs.SetFloat("Music Volume", musicVolume.value);
        PlayerPrefs.SetFloat("Sound Effects Volume", soundEffectsVolume.value);
        PlayerPrefs.SetFloat("Initial Countdown Duration", initialCountdownDuration.value);
        PlayerPrefs.SetFloat("Match Duration", matchDuration.value);
        BGMAudioSource.volume = PlayerPrefs.GetFloat("Music Volume");

        mainMenuPanel.SetActive(true);
    }


    public void UpdateValues()
    {
        ICDText.text = ((int)(initialCountdownDuration.value)).ToString() + " seconds";
        MDText.text = ((int)(matchDuration.value)).ToString() + " seconds";
    }
}
