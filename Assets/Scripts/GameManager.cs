using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }
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
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.lives = 3;
        this.coins = 0;
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }
    private void GameOver()
    {
        NewGame();
    }

    public void ResetLevel()
    {
        this.lives--;
        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            LoadLevel(this.world, this.stage);
        }
    }
    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }
    public void NextLevel()
    {
        LoadLevel(this.world, this.stage + 1);
    }

    public void AddCoin()
    {
        this.coins++;
        if (coins >= 100)
        {
            AddLife();
            this.coins -= 100;
        }
    }

    public void AddLife()
    {
        this.lives++;
    }


}
