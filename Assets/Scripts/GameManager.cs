using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI countDownText;
    public float countDownTime = 120f;
    public Transform enemy;
    public Transform enemy2;
    public Transform enemy3;
    public Transform shooterEnemy;
    public Transform shooterEnemy2;
    public Transform shooterEnemy3;
    public Transform shooterEnemy4;
    public Transform shooterEnemy5;
    public Transform shooterEnemy6;
    public Transform bossEnemy;
    public GameObject pauseMenuUi;
    public bool isPaused = false;
    private List<AudioSource> audioSources = new List<AudioSource>();
    public List<AudioSource> excludedAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        countDownText.text = "Time: " + countDownTime.ToString("F1") + "s";
    }

    void Update()
    {
        countDownText.text = "Time: " + countDownTime.ToString("F1") + "s";
        if (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
        }
        else
        {
            SpawnBoss();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }

            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseMenuUi.SetActive(true);
        PauseAudio();
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUi.SetActive(false);
        ResumeAudio();
        Time.timeScale = 1f;
    }

    void PauseAudio()
    {
        foreach (AudioSource audio in FindObjectsOfType<AudioSource>())
        {
            if (!excludedAudioSources.Contains(audio) && audio.isPlaying)
            {
                audio.Pause();
                audioSources.Add(audio);
            }
        }
    }

    void ResumeAudio()
    {
        for (int i = audioSources.Count - 1; i >= 0; i--)
        {
            if (audioSources[i])
            {
                audioSources[i].UnPause();
                audioSources.RemoveAt(i);
            }
        }
    }

    public void QuitToMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void SpawnBoss()
    {
        if (enemy != null && enemy2 != null && enemy3 != null && shooterEnemy != null && shooterEnemy2 != null && shooterEnemy3 != null && shooterEnemy4 != null && shooterEnemy5 != null && shooterEnemy6 != null)
        {
            Destroy(enemy.gameObject);
            Destroy(enemy2.gameObject);
            Destroy(enemy3.gameObject);
            Destroy(shooterEnemy.gameObject);
            Destroy(shooterEnemy2.gameObject);
            Destroy(shooterEnemy3.gameObject);
            Destroy(shooterEnemy4.gameObject);
            Destroy(shooterEnemy5.gameObject);
            Destroy(shooterEnemy6.gameObject);

            Instantiate(bossEnemy, new Vector3(0, 8, 0), Quaternion.identity);
        }
    }
}
