using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SpawnController : MonoBehaviour
{
    //[HideInInspector]
    public bool pTrigger;
    private float timer;
    private int astroidsPath;//entexabe masir movvarrab
    private int i;
    public float n;//baraye entexab masir
    private readonly float[] Arraytemp = { -2.5f, 5f, -5f, 2.5f };

    [HideInInspector] public float temp;//adade komaki
    [HideInInspector] public float temp2;//adade komaki
    [HideInInspector] public float Enemy2ForwardPosition;
    [HideInInspector] public int sign;
    //[HideInInspector] public int Life;
    //[HideInInspector] public bool checkLife;
    [HideInInspector] public bool booltemp;//motaghayere komaki besorate boolian

    public GameObject[] Hazards;
    public GameObject[] Powerups;
    public GameObject[] obliqueSpawnPos;//spawn position haye movarrab


    private Vector3 SpawnPosition;
    private Quaternion SpawnRotation;
    public Vector3 SpawnValues;
    public float StartWait;


    public Text RoundText;
    private int Round = 1;
    private GameObject hazard;
    public DontDestroyOnLoad checkPoint;
    public GameObject NextlevelText;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        checkPoint = GameObject.FindGameObjectWithTag("DontDestroyOnLoad").GetComponent<DontDestroyOnLoad>();
    }
    void Start()
    {
        n = 0;
        pTrigger = true;//its for not bieng respawn several of powerups together
    }
    IEnumerator SpawnWaves()
    {
        //level 1
        if (checkPoint.lastcheckpoint == 0)
        {
            
            yield return new WaitForSeconds(StartWait);
            RoundText.text = "Level " + Round;
            for (i = 0; i < 15; i++)
            {
                hazard = Hazards[Random.Range(0, 3)];
                SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()
                Instantiate(hazard, SpawnPosition, SpawnRotation);
                yield return new WaitForSeconds(0.75f);
            }

            //                                                ****************************************

            yield return new WaitForSeconds(4f);
            //Instantiate(Hazards[5], SpawnPosition, SpawnRotation);
            yield return new WaitForSeconds(3f);
            for (i = 0; i < 40; i++)
            {

                // these are the Percentages of spawning for our objects (enemy's)
                if (Random.value <= 0.2)
                {
                    hazard = Hazards[Random.Range(0, 3)];
                }
                else if (Random.value <= 0.7)
                {
                    hazard = Hazards[3];
                }
                /*else if (Random.value <= 0.2)
                {
                    if (pTrigger == true)
                    {
                        hazard = Hazards[Random.Range(5, 9)];
                        pTrigger = false;
                        StartCoroutine(PowerUpSpawnWait());//its for not bieng respawn several of them together
                    }
                    else
                        continue;
                }*/
                else continue;
                SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()
                if (hazard)
                {
                    Instantiate(hazard, SpawnPosition, SpawnRotation);
                }
                yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            }

            //                                                ****************************************

            yield return new WaitForSeconds(5f);
            n = 1;
            for (i = 0; i < 16; i++)
            {
                Instantiate(Hazards[3]);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************

            yield return new WaitForSeconds(1f);
            n = 10;
            temp = 5;
            for (i = 0; i < 10; i++)
            {

                if (i % 2 == 0)
                {
                    Instantiate(Hazards[3], new Vector3(7f, 8f, 10f), Quaternion.identity);
                }
                else
                {
                    Instantiate(Hazards[3], new Vector3(-7f, 8f, 7f), Quaternion.identity);
                    yield return null;
                    temp -= 2.4f;
                }
                yield return new WaitForSeconds(0.7f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
            //                                                ****************************************

            yield return new WaitForSeconds(1f);
            n = 11;
            temp = 5;
            sign = 1;
            for (i = 0; i < 8; i++)
            {

                Instantiate(Hazards[3], new Vector3(0f, 8f, 21f), Quaternion.identity);
                yield return new WaitForSeconds(0.7f);
                temp += 2;
                sign *= (-1);
            }
            //Instantiate(Hazards[Random.Range(5,7)], SpawnPosition, SpawnRotation);
            yield return new WaitForSeconds(2f);
            temp = 5;
            sign = 1;
            for (i = 0; i < 8; i++)
            {

                Instantiate(Hazards[3], new Vector3(0f, 8f, 21f), Quaternion.identity);
                yield return new WaitForSeconds(0.7f);
                temp += 2;
                sign *= (-1);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************

            timer = Time.time + 20;
            while (timer > Time.time)
            {
                astroidsPath = Random.Range(0, obliqueSpawnPos.Length);
                hazard = Hazards[Random.Range(0, 3)];
                Instantiate(hazard, obliqueSpawnPos[astroidsPath].transform.position, obliqueSpawnPos[astroidsPath].transform.rotation);
                yield return new WaitForSeconds(0.3f);
                Debug.Log(Time.time);
            }
            Round++;

            yield return new WaitForSeconds(4f);
            checkPoint.lastcheckpoint = 1;
        }

        //###################################################################### LEVEL 2 ##########################################################

        //level 2
        if (checkPoint.lastcheckpoint == 1)
        {
            NextlevelText.SetActive(true);
            yield return new WaitForSeconds(3f);
            NextlevelText.SetActive(false);
            Enemy2ForwardPosition = 16;
            Instantiate(Hazards[4], new Vector3(3, 8, 21), Quaternion.identity);
            Instantiate(Hazards[4], new Vector3(-3, 8, 21), Quaternion.identity);
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************

            yield return new WaitForSeconds(1f);
            //Instantiate(Hazards[9], SpawnPosition, SpawnRotation);
            Enemy2ForwardPosition = 16;
            temp = 0;
            StartCoroutine(Enemy1Spawn());
            for (i = 0; i < 30; i++)
            {
                SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()

                if (i % 8 == 0)
                    Instantiate(Hazards[4], new Vector3(Arraytemp[(int)temp++], 8, 22), Quaternion.identity);
                /*else if (Random.value <= 0.5)
                {
                    Instantiate(Hazards[Random.Range(0, 3)], SpawnPosition, SpawnRotation);
                }*/
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************
            yield return new WaitForSeconds(1f);
            n = 3;
            for (i = 0; i < 20; i++)
            {
                Instantiate(Hazards[3]);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************
            yield return new WaitForSeconds(1f);
            n = 12;
            temp = 5;
            for (i = 0; i < 12; i++)
            {

                if (i % 2 == 0)
                {
                    Instantiate(Hazards[3], new Vector3(7f, 8f, 10f), Quaternion.identity);
                }
                else
                {
                    Instantiate(Hazards[3], new Vector3(-7f, 8f, 7f), Quaternion.identity);
                    yield return null;
                    temp -= 2f;
                }
                yield return new WaitForSeconds(0.3f);
            }
            //Instantiate(Hazards[6], SpawnPosition, SpawnRotation);
            booltemp = true;
            yield return null;
            Enemy2ForwardPosition = 15;
            Instantiate(Hazards[4], new Vector3(2, 8, 21), Quaternion.identity);
            Instantiate(Hazards[4], new Vector3(-2, 8, 21), Quaternion.identity);
            booltemp = false;
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************
            yield return new WaitForSeconds(1f);
            StartCoroutine(Enemy2SpawnWithDelay());
            //Instantiate(Hazards[5], SpawnPosition, SpawnRotation);
            for (i = 0; i < 25; i++)
            {
                if (i % 2 == 0)
                    n = 1;
                else
                    n = 2;

                Instantiate(Hazards[3]);
                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************
            yield return new WaitForSeconds(1f);
            for (i = 0; i < 20; i++)
            {
                hazard = Hazards[Random.Range(0, 3)];
                SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()
                Instantiate(hazard, SpawnPosition, SpawnRotation);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            //                                                ****************************************

            yield return new WaitForSeconds(1f);
            //StartCoroutine(EnemyRandomSpawn(25f));
            StartCoroutine(Enemy1Circle());
            Enemy2ForwardPosition = 11;
            Instantiate(Hazards[4], new Vector3(-3.3f, 8.2f, 21f), Quaternion.identity);
            Instantiate(Hazards[4], new Vector3(3.3f, 8.2f, 21f), Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Enemy2ForwardPosition = 16;
            Instantiate(Hazards[4], new Vector3(0f, 8.2f, 21f), Quaternion.identity);
            yield return new WaitForSeconds(4f);
            Round++;
        }
    }
    //#####################################################
    public void StartGame()
    {
        StartCoroutine(SpawnWaves());
        StartCoroutine(PowerUpSpawner());
    }
    //##########################################################
    IEnumerator PowerUpSpawnWait()
    {
        pTrigger = false;
        yield return new WaitForSecondsRealtime(Random.Range(30f, 40f));
        pTrigger = true;
    }
    //#########################################################
    IEnumerator Enemy1Spawn()
    {
        yield return new WaitForSeconds(5f);
        sign = 1;
        n = 13;
        temp2 = 5;
        for(int x=0;x<6;x++)
        {
            Instantiate(Hazards[3], new Vector3(7f * sign, 8f, -3f), Quaternion.identity);
            sign *= (-1);
            //timer = 1.5f;
            temp2 += 2;
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(10f);
        n = 14;
        temp2 =5f;
        for (int x = 0; x < 6; x++)
        {

            if (x % 2 == 0)
            {
                Instantiate(Hazards[3], new Vector3(7f, 8f, -3f), Quaternion.identity);
            }
            else
            {
                Instantiate(Hazards[3], new Vector3(-7f, 8f, -3f), Quaternion.identity);
                yield return new WaitForSeconds(0.7f);
                temp2 -= 2f;
            }
            
        }
        yield break;
    }
    IEnumerator Enemy1Circle()
    {
        yield return new WaitForSeconds(3f);
        
        for (i=0;i<14;i++)
        {
            if (i % 2 == 0)
            {
                n = 4;
                Instantiate(Hazards[3], new Vector3(7f, 8f, 14.25f), Quaternion.identity);
                yield return null;
            }
            else
            {
                n = 5;
                Instantiate(Hazards[3], new Vector3(-7f, 8f, 14.25f), Quaternion.identity);
                yield return new WaitForSeconds(0.7f);
            }
        }
    }
    IEnumerator EnemyRandomSpawn(float k)
    {
        for (int j = 0; j < k; j++)
        {

            // these are the Percentages of spawning for our objects (enemy's)
            if (Random.value <= 0.2)
            {
                hazard = Hazards[Random.Range(0, 3)];
            }
            else continue;
            SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
            SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()
            if (hazard)
            {
                Instantiate(hazard, SpawnPosition, SpawnRotation);
            }
            yield return new WaitForSeconds(0.8f);
        }
    }
    IEnumerator Enemy2SpawnWithDelay()
    {
        yield return new WaitForSeconds(2f);
        Enemy2ForwardPosition = 10;
        //Instantiate(Hazards[6], SpawnPosition, SpawnRotation);
        Instantiate(Hazards[4], new Vector3(0f, 8.2f, 21f), Quaternion.identity);
    }

    IEnumerator PowerUpSpawner()
    {
        while(true)
        {
            SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
            SpawnRotation = Quaternion.identity;//it work with out identity too like this: new Quaternion()
            if(Random.value<=0.04)
                Instantiate(Powerups[Random.Range(3, 5)], SpawnPosition, SpawnRotation);
            else if(Random.value<=0.4)
                Instantiate(Powerups[Random.Range(0,3)], SpawnPosition, SpawnRotation);
            yield return new WaitForSecondsRealtime(Random.Range(25f, 35f));
        }
    }
    public void startPowerUpSpawnWaitFunc()
    {
        StartCoroutine(PowerUpSpawnWait());
    }
}
