using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SurvivleSpawnController : MonoBehaviour 
{
    private bool pTrigger;
    
    public GameObject[] hazards;
	public Vector3 SpawnValues;
	public int hazardcount;
	public float SpawnWait;
	public float StartWait;
	public float WaveWait;


    public Text RoundText;
    private int Round=1;
    private GameObject hazard;

    private void Awake()
    {
        Application.targetFrameRate=60;
    }
    void Start()
	{
        pTrigger = true;
	}
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (StartWait);
		while (true) 
		{
            RoundText.text = "Round " + Round;
			for (int i = 0; i < hazardcount; i++) {

                // these are the Percentages of spawning for our objects (enemy's)
                if (Random.value <= 0.4)
                {
                    hazard = hazards[Random.Range(0, 2)];
                }
                else if (Random.value <= 0.3)
                {
                    hazard = hazards[3];
                }
                else if (Random.value <= 0.2)
                {
                    hazard = hazards[4];
                }
                else if (Random.value <= 0.2)
                {
                    if (pTrigger == true)
                    {
                        hazard = hazards[Random.Range(5, 9)];
                        pTrigger = false;
                        StartCoroutine(PowerUpSpawnWait());//its for not bieng respawn several of them together
                    }
                    else
                        continue;
                }
                else continue;
                Vector3 SpawnPosition = new Vector3 (Random.Range (-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
				Quaternion SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()
				if (hazard) {
					Instantiate (hazard, SpawnPosition, SpawnRotation);
				}
				yield return new WaitForSeconds (SpawnWait);
			}
			yield return new WaitForSeconds (WaveWait);
			hazardcount += 10;
			SpawnWait -= 0.05f;
            Round++;
			/*if (gameOver) 
			{
				//restartText.text = "Press 'R' for Restart";
				//restart = true;
				restartButton.SetActive(true);
				break;
			}*/
		}
	}
    public void StartGame()
    {
        StartCoroutine(SpawnWaves());
    }
    IEnumerator PowerUpSpawnWait()
    {
        yield return new WaitForSecondsRealtime(Random.Range(5f, 8f));
        pTrigger = true;
    }
}
