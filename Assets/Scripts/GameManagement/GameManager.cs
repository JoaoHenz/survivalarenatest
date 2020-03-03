using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static Player player;
    public static GameManagerSheet sheet;
    public static AudioSource itemAudioSource;
    public static GameObject[] strategicPoints;

    static GameManager instance;
    static List<Transform> spawnPoints = new List<Transform>();
    static float time;

    public enum CronogramType { Auto,Manual};
    public CronogramType cronogramType;
    public GameObject spawnPointsObject;
    public GameManagerSheet gameManagerSheet;
    public AudioClip newWaveSound;
    public AudioClip BGM;
    public Text newWaveText;
    public AudioSource audioSource;
    public AudioSource BGMAudio;
    public AudioSource itemAS;
    public Text importantText;
    public AudioClip gameOver;
    public AudioClip congratulations;
    public GameObject pausePanel;
    public Text countDownText;
    public List<string> waves;
    public Text importantText2;

    bool gameIsPaused = false;
    float countdown;
    GameObject[] enemies;
    bool forcedPause= false;


    void Start () {
        instance = this;
        sheet = gameManagerSheet;
        itemAudioSource = itemAS;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        time = 0f;
        spawnPoints = new List<Transform>();
        foreach (Transform child in spawnPointsObject.transform)
        {
            spawnPoints.Add(child);
        }
        strategicPoints = GameObject.FindGameObjectsWithTag("Strategic Point");
        int currentWave = 0;
        waves = new List<string>();

        if (cronogramType == CronogramType.Manual)
            waves = sheet.spawnCronogram.cronogram;
        else
            waves = buildAutoCronogram();

        StartCoroutine(StartingCooldown(waves, currentWave));
        BGMAudio.clip = BGM;
        if(PlayerPrefs.HasKey("Sound Effects Volume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("Sound Effects Volume");
            itemAS.volume = PlayerPrefs.GetFloat("Sound Effects Volume");
        }
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            BGMAudio.volume = PlayerPrefs.GetFloat("Music Volume");
        }
        BGMAudio.Play();
        BGMAudio.loop = true;
        string[] nextWave = waves[0].Split('/');
        string[] timeInfo = nextWave[0].Split(':');
        float timeNextWave = int.Parse(timeInfo[0]) * 60 + int.Parse(timeInfo[1]);
        countdown = timeNextWave;

    }

    List<string> buildAutoCronogram()
    {
        List<string> cronogram = new List<string>();
        float matchDuration = 0f;
        float initialCountDownDuration = 0f;

        if (PlayerPrefs.HasKey("Match Duration")) matchDuration = PlayerPrefs.GetFloat("Match Duration");
        if (PlayerPrefs.HasKey("Initial Countdown Duration")) initialCountDownDuration = PlayerPrefs.GetFloat("Initial Countdown Duration");
        matchDuration += initialCountDownDuration;

        int numberWaves = (int)(matchDuration) / 30;
        if (numberWaves <= 0) numberWaves = 1;


        for(int i = 0; i<numberWaves; i++)
        {
            int waveTimeSec = i * 30 + (int)initialCountDownDuration;
            int waveTimeMin = waveTimeSec / 60;
            waveTimeSec %= 60;
            string wave = waveTimeMin.ToString() + ":" + waveTimeSec.ToString() + "/Warrior:" + ((i+1) * 5).ToString();
            if (i % 2==0 && i>0)
            {
                wave += "/Archer:" + (i * 2).ToString();
            }
            if (i % 5 == 0)
            {
                wave += "/Gojira:" + (i+1).ToString();
            }

            cronogram.Add(wave);
        }

        return cronogram;


    }

    void Update()
    {
        if (!gameIsPaused)
        {
            time += Time.deltaTime;
            countdown -= Time.deltaTime;
            if (countdown > 0)
            {
                countDownText.text = ((int)countdown).ToString();
            }
            else
            {
                countDownText.text = "";
            }
            
        }

        if (!forcedPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!gameIsPaused)
                    PauseGame();
                else
                    UnpauseGame();
            }
        }


    }

    void PauseGame()
    {
        gameIsPaused = true;
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            enemy.SetActive(false);
        player.rigid.useGravity = false;
        player.gameObject.SetActive(false);

    }

    public void UnpauseGame()
    {
        gameIsPaused = false;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        foreach (GameObject enemy in enemies)
            enemy.SetActive(true);
        player.rigid.useGravity = true;
        player.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

    IEnumerator StartingCooldown(List<string> waves, int currentWave)
    {
        string[] nextWave = waves[0].Split('/');
        string[] timeInfo = nextWave[0].Split(':');
        float timeNextWave = int.Parse(timeInfo[0]) * 60 + int.Parse(timeInfo[1]);

        while (time < timeNextWave)
        {
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(HandleWave(waves, currentWave));
    }


    IEnumerator HandleWave(List<string> waves, int currentWave)
    {

        StartCoroutine(newWaveSequence(currentWave));

        string[] wave = waves[currentWave].Split('/');

        for(int i = 1; i < wave.Length; i++)
        {
            string[] monsterInfo = wave[i].Split(':');
            string monsterName = monsterInfo[0].ToLower();
            int monsterQuantity = int.Parse(monsterInfo[1]);
            GameObject monsterType = Resources.Load<GameObject>("Standard/Prefabs/characters/monsters/" + monsterName);
            int k = 0;
           for (int j = 0; j < monsterQuantity; j++)
            {
                GameObject monster;
                monster = Instantiate(monsterType);
                monster.transform.position = spawnPoints[k].position;

                if (k + 1 < spawnPoints.Count) k++; else k = 0;
            }
        }

        if (currentWave + 1 < waves.Count)
        {
            string[] nextWave = waves[currentWave + 1].Split('/');
            string[] timeInfo = nextWave[0].Split(':');
            float timeNextWave = int.Parse(timeInfo[0]) * 60 + int.Parse(timeInfo[1]);

            while (time < timeNextWave)
            {
                yield return new WaitForSeconds(1f);
            }


            StartCoroutine(HandleWave(waves, currentWave + 1));
        }
        else
        {
            newWaveText.text = "Finish the enemies!!!";
            while (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
            {
                yield return new WaitForSeconds(5f);
            }
            StartCoroutine(congratulationsSequence());
        }

    }




    IEnumerator newWaveSequence(int currentWave)
    {
        audioSource.clip = newWaveSound;
        audioSource.Play();
        newWaveText.text = "Wave " + currentWave.ToString() + "!!!";

        yield return new WaitForSeconds(3f);

        newWaveText.text = "";
    }

    public static void handlePlayerDeath()
    {
        instance.StartCoroutine(instance.gameOverSequence());
    }

    IEnumerator gameOverSequence()
    {
        forcedPause = true;
        gameIsPaused = true;
        player.rigid.useGravity = false;
        player.gameObject.SetActive(false);
        newWaveText.text = "";
        importantText.text = "GAME OVER";
        BGMAudio.clip = gameOver;
        BGMAudio.Play();
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("cemetery");
    }

    IEnumerator congratulationsSequence()
    {
        forcedPause = true;
        gameIsPaused = true;
        player.rigid.useGravity = false;
        player.gameObject.SetActive(false);
        newWaveText.text = "";
        importantText.text = "CONGRATULATIONS";
        importantText2.text = "Score: " + player.Score.ToString() + " Kills: " + player.enemiesKilled.ToString();
        BGMAudio.clip = congratulations;
        BGMAudio.Play();
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("mainmenu");
    }
	
}
