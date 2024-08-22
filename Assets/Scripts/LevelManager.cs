using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float levelDuration = 60.0f;
    float countDown = 0;
    public static bool isGameOver = false;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    public string nextlevel;
    public Text timerText;
    public Text gameText;
    public Text scoreText;
    public Text witchScore;
    private int score = 0;
    private int witchScoreNum = 0;
    public float fallThreshold = -.5f;
    public GameObject player;
    private AudioSource audioSource;
    GameObject spawner;

    void Start()
    {
        isGameOver = false;
        countDown = levelDuration;
        SetTimerText();
        UpdateScoreText();
        UpdateWitchScoreText();
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Witch");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        spawner.SetActive(true);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found. Please add an AudioSource to this GameObject.");
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
            }
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                countDown = 0.0f;
                LevelLost();
            }
            SetTimerText();
        }
        if (player != null && player.transform.position.y < fallThreshold)
        {
            LevelLost();
        }
    }

    void SetTimerText()
    {
        timerText.text = countDown.ToString("f2");
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        timerText.text = "0";
        gameText.gameObject.SetActive(true);

        if (audioSource != null && gameOverSFX != null)
        {
            audioSource.PlayOneShot(gameOverSFX);
        }
        else
        {
            Debug.LogWarning("AudioSource or gameOverSFX is null. Sound won't play.");
        }

        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Witch");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
        
        spawner.SetActive(false);
        isGameOver = true;
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);

        if (audioSource != null && gameWonSFX != null)
        {
            audioSource.PlayOneShot(gameWonSFX);
        }
        else
        {
            Debug.LogWarning("AudioSource or gameWonSFX is null. Sound won't play.");
        }

        if (SceneManager.GetActiveScene().name.Contains("3"))
        {
            gameText.text = "CONGRATULATIONS! YOU COMPLETED THE GAME!";
        }
        else if (!string.IsNullOrEmpty(nextlevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextlevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateWitchScoreText()
    {
        witchScore.text = "Witch Kill Score: " + witchScoreNum.ToString();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        Debug.Log("Adding Score");
    }

    public void AddWitchScore(int value) 
    {
        witchScoreNum += value;
        UpdateWitchScoreText();
    }
}