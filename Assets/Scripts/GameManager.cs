using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public BottleSpawnManager bottleSpawn;

    private GameObject currentBottleObj;
    public GameObject ResultCanvas;
    public GameObject PauseCanvas;
    public GameObject HUDCanvas;

    public Object StarPrefab;

    private Bottle currentBottle;
    public Bottle CurrentBottle {
        get {
            return currentBottle;
        }
    }

    private float balancedTime = 0f;
    public float clearTime = 6.0f;

    private Vector3 prevLaundryPos;
    private Vector3 currentLaundryPos;

    private int numberOfStar = 0;
    private int currentLevel;
    public int CurrentLevel {
        set {
            currentLevel = value;
        } get {
            return currentLevel;
        }
    }

    private bool isGameOver = false;
    public bool IsGameOver {
        get {
            return isGameOver;
        }
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        currentLevel = ApplicationManager.instance.SelectedLevel;
        GameStart();
	}
	
	// Update is called once per frame
	void Update () {
        if (isGameOver)
            return;

		if(currentBottle != null && currentBottle.IsThrowed) {
            if(currentBottle.IsGrounded && currentBottle.transform.rotation.z * Mathf.Rad2Deg <= 1.0f && currentBottle.transform.rotation.z * Mathf.Rad2Deg >= -1.0f) {
                balancedTime += Time.deltaTime;
            } else {
                balancedTime = 0f;
            }

            if (currentBottle.transform.position.x < 7.0f && balancedTime >= 4.0f) {
                GameObject.Find("txtCount").GetComponent<Text>().text = "1";
            } else if (currentBottle.transform.position.x < 7.0f && balancedTime >= 3.0f) {
                GameObject.Find("txtCount").GetComponent<Text>().text = "2";
            } else if (currentBottle.transform.position.x < 7.0f && balancedTime >= 2.0f) {
                GameObject.Find("txtCount").GetComponent<Text>().text = "3";
            } else if(currentBottle.transform.position.x >= 7.0f && balancedTime >= 1.0f) {
                Destroy(currentBottleObj);
                SpawnBottle();
            } else {
                GameObject.Find("txtCount").GetComponent<Text>().text = "";
            }

            if (balancedTime >= clearTime) {
                if (currentBottle.transform.position.x < 7.0f) {
                    GameOver();
                } else {
                    
                    Destroy(currentBottleObj);
                    SpawnBottle();
                }
            }

            prevLaundryPos = currentBottle.transform.position;
        }
	}

    public void SpawnBottle() {
        currentBottleObj = bottleSpawn.SpawnBottle();
        currentBottle = currentBottleObj.GetComponent<Bottle>();

        balancedTime = 0f;

        GameObject.Find("txtCount").GetComponent<Text>().text = "";

        foreach(GameObject star in GameObject.FindGameObjectsWithTag("Star")) {
            Destroy(star);
        }

        numberOfStar = 0;
        foreach(GameObject starSpawnPos in GameObject.FindGameObjectsWithTag("StarSpawnPos")) {
            Instantiate(StarPrefab, starSpawnPos.transform.position, starSpawnPos.transform.rotation);
        }
    }

    public void ThrowLaundry(Vector2 dir) {
        currentBottle.GetComponent<Bottle>().Throw(dir);
    }

    public void GetStar() {
        numberOfStar++;
    }

    public void GameOver() {
        Time.timeScale = 0f;
        isGameOver = true;
        ResultCanvas.GetComponent<ResultCanvasManager>().SetStar(numberOfStar);
        ResultCanvas.GetComponent<Canvas>().sortingLayerName = "MenuUI";

        int lastLevel;
        if (currentLevel >= ApplicationManager.instance.LastLevel) {
            lastLevel = currentLevel + 1;
            ApplicationManager.instance.LastLevel = currentLevel + 1;
        } else
            lastLevel = ApplicationManager.instance.LastLevel;

        int[] stars = ApplicationManager.instance.Stars;
        if(numberOfStar > stars[currentLevel - 1]) {
            stars[currentLevel - 1] = numberOfStar;
            ApplicationManager.instance.Stars[currentLevel - 1] = numberOfStar;
        }

        Debug.Log("Save : " + lastLevel + ", " + numberOfStar);

        BinaryDataManager.SaveLevelsInfoData(lastLevel, stars, ApplicationManager.instance.IsLoggedIn, ApplicationManager.instance.UserId);
    }

    public void GameStart() {

        Debug.Log("GameStart");
        Time.timeScale = 1f;
        isGameOver = false;
        ResultCanvas.GetComponent<Canvas>().sortingLayerName = "Hide";
        PauseCanvas.GetComponent<Canvas>().sortingLayerName = "Hide";

        bottleSpawn = GameObject.Find("BottleSpawn").GetComponent<BottleSpawnManager>();

        Destroy(currentBottleObj);
        SpawnBottle();
    }

    public void NextLevel() {
        if (currentLevel >= 30) {
            MainMenu();
            return;
        }

        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        currentLevel++;
        Debug.Log(currentLevel);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level" + currentLevel);

        yield return asyncOperation;
        ResultCanvas.GetComponent<Canvas>().sortingLayerName = "Hide";
        GameStart();
    }

    IEnumerator LoadMainMenuLoading() {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenuLoading");

        yield return asyncOperation;

        Destroy(ResultCanvas);
        Destroy(PauseCanvas);
        Destroy(HUDCanvas);
        Destroy(gameObject);
    }

    public void MainMenu() {
        StartCoroutine(LoadMainMenuLoading());
    }

    public void Pause() {
        isGameOver = true;
        Time.timeScale = 0f;
        PauseCanvas.GetComponent<Canvas>().sortingLayerName = "MenuUI";
    }

    public void Play() {
        isGameOver = false;
        Time.timeScale = 1f;
        PauseCanvas.GetComponent<Canvas>().sortingLayerName = "Hide";
    }
}
