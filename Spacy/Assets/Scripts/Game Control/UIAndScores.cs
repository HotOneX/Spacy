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
    public Renderer rend;
    public GameObject colormenu;

    public GameObject pauseUI;
    public static int[] powerCounts = { 0, 0, 0 };
    public TextMeshProUGUI[] PowerUpText;

    public Slider BulletLevelupSlider;
    public Text scoreText;
    public GameObject gameOverText;
    public GameObject restartButton;

    public GameObject Player1;
    public GameObject Player2;

    private int score;
    private float SliderOldValue=0;
    public float Timer=0f;

    public SpawnController spawnController;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        //rend = Player1.GetComponent<Renderer>();
    }
    void Start()
    {
        GameisStarted = false;
        restartButton.SetActive(false);
        colormenu.SetActive(true);
        score = 0;
        AddScore(0);
        BulletLevelupSlider.value = 0;
        BulletLevelupSlider.maxValue = 500 + PlayerController.weaponLevel * 200;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverGameObject())
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
        if(BulletLevelupSlider.value==BulletLevelupSlider.maxValue && PlayerController.weaponLevel < 14)
        {
            PlayerController.weaponLevel++;
            BulletLevelupSlider.maxValue = 600 + PlayerController.weaponLevel * 400;
            BulletLevelupSlider.value = 0;
        }
        if (BulletLevelupSlider.value == SliderOldValue)
        {
            Timer += Time.deltaTime;
            if (Timer >= 1f)
            {
                BulletLevelupSlider.value -= PlayerController.weaponLevel * 2;
                SliderOldValue = BulletLevelupSlider.value;
            }
        }
        else
        {
            Timer = 0;
            SliderOldValue = BulletLevelupSlider.value;
        }
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
    public static bool IsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }
}
