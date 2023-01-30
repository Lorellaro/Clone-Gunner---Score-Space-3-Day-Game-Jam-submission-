using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // keep track of score
    public static int score;
    public Text scoreText;
    public Text finalLivesText;
    public Text finalScoreText;
    public Button mainMenuButton;
    public Button retryButton;
    public GameObject highScoreContainer;
    public Text highScoreText;
    public TMP_InputField nameInput;
    public GameObject gameOverContainer;
    public GameObject pauseMenuContainer;
    public GameObject[] livesContainer;
    public GameObject orbsContainer;
    [SerializeField] AudioSource buttonClickedSFX;
    int playerLives;
    public leaderboard lb;


    private void Start()
    {
        Time.timeScale = 1;
        score = 0;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        playerLives = PlayerScript.lives;
        scoreText.text = "Score: " + score.ToString("D10");
        // display lives
        livesHUD(playerLives);
        // player lives drop below 1
        if (playerLives < 1)
        {
            EndGame();
        }
        // orb container is empty
        if (orbsContainer.transform.childCount <= 0)
        {
            EndGame();
        }
    }

    void livesHUD(int lives)
    {
        // set all lives false
        foreach (GameObject life in livesContainer)
        {
            life.SetActive(false);
        }
        // re enable up to lives  
        for (int i = 0; i < lives; i++)
        {
            livesContainer[i].SetActive(true);
        }
    }

    public void NewGame()
    {
        updateLeaderBoard();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void EndGame()
    {
        if (playerLives > 0)
        {
            // turn final lives on
            finalLivesText.text = livesContainer.Length.ToString();
        }

        // display game over screen use invoke to wait for player to explode
        StartCoroutine(invokeGameOver());
        Time.timeScale = 0;
    }

    void GameOverScreen()
    {
        // stop displaying hud
        scoreText.enabled = false;
        // display game over screen
        gameOverContainer.SetActive(true);
        if (playerLives > 0)
        {
            // Display remaining lives as a text
            finalLivesText.enabled = true;
        }

        for (int i = lb.scores.Length - 1; i < 0; i--)
        {
            if (score > lb.scores[i])
            {
                highScoreText.text = "New HighScore at Rank " + (i + 1).ToString();
                highScoreContainer.SetActive(true);
            }
        }

        // display final score
        finalScoreText.text = score.ToString("D10");

    }

    public void mainMenu()
    {
        updateLeaderBoard();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void buttonHoverEnter(GameObject parentButton)
    {
        parentButton.GetComponent<AudioSource>().Play();
        parentButton.transform.GetChild(1).gameObject.SetActive(true);
        parentButton.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    public void buttonHoverExit(GameObject parentButton)
    {
        parentButton.transform.GetChild(1).gameObject.SetActive(false);
        parentButton.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Normal;
    }

    public void Resume()
    {
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1f;
    }
    IEnumerator invokeGameOver()
    {
        yield return new WaitForSecondsRealtime(0.75f);
        Invoke("GameOverScreen", 0);
    }

    private void updateLeaderBoard()
    {
        string name = nameInput.text;

        //Loop trough array and compare scores
        for (int i = 0; i < lb.scores.Length; i++)
        {
            //if score is bigger than position at index
            if (score > lb.scores[i])
            {
                //move all lower scores & names down by 1
                for (int j = lb.scores.Length - 2; j == i; j--)
                {
                    lb.scores[j + 1] = lb.scores[j];
                    lb.names[j + 1] = lb.names[j];
                }
                // slot in score  & name in correct positon
                lb.scores[i] = score;
                lb.names[i] = name;
                break;
            }
        }
    }
}
