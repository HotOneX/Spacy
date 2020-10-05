using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIAndScores : MonoBehaviour
{
    [HideInInspector]
    public static bool GameisStarted;
    public Material[] materials1;
    public Material[] materials2;
    public Material[] materials3;
    private Renderer rend;
    public GameObject colormenu;

    public Text scoreText;
    public GameObject gameOverText;
    //public GameObject levelCompleteText;
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
    // Start is called before the first frame update
    void Start()
    {
        GameisStarted = false;
        restartButton.SetActive(false);
        colormenu.SetActive(true);
        score = 0;
        AddScore(0);
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
