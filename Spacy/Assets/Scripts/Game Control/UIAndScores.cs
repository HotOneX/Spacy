using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class UIAndScores : MonoBehaviour
{
    [HideInInspector]
    public static bool GameisStarted;
    public Material[] materials1;
    public Material[] materials2;
    public Material[] materials3;
    private Renderer rend;
    public GameObject colormenu;

    public GameObject pauseUI;
    public static int[] powerCounts = { 0, 0, 0 };
    public TextMeshProUGUI[] PowerUpText;

    public Text scoreText;
    public GameObject gameOverText;
    public GameObject restartButton;

    public GameObject Player1;
    public GameObject Player2;

    private int score;

    public SpawnController spawnController;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        rend = Player1.GetComponent<Renderer>();
    }
    void Start()
    {
        GameisStarted = false;
        restartButton.SetActive(false);
        colormenu.SetActive(true);
        score = 0;
        AddScore(0);
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (pauseUI.activeSelf == true)
            {
                pauseUI.SetActive(false);
            }
            if(PlayerController.newPower==1)
            {
                Time.timeScale = 0.2f;
            }
            else Time.timeScale = 1f;
        }
        else
        {
            if (GameisStarted && pauseUI.activeSelf == false)
            {
                pauseUI.SetActive(true);
            }
            Time.timeScale = 0.1f;
        }
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }



    public void AddScore(int NewScoreValue)
    {
        score += NewScoreValue;
        scoreText.text = "Score:" + score;
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        StartCoroutine(SetActiveRestart());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void ChangeMaterial(int i) // its for change the color of Player SpaceShip
    {
        if (i == 0)
        {
            rend.materials = materials1;// materials1 is brown

        }
        else if (i == 1)
        {
            rend.materials = materials2;// materials2 is Olive(Greend)
        }
        else if (i == 2)
        {
            rend.materials = materials3;// materials3 is Dark
        }
        else if (i == 3)//its for change the Player Spaceship
        {
            Player1.SetActive(false);
            Player2.SetActive(true);
        }
        colormenu.SetActive(false);
        GameisStarted = true;
        spawnController.StartGame();
    }
    IEnumerator SetActiveRestart()
    {
        yield return new WaitForSeconds(3f);
        restartButton.SetActive(true);
    }
}
