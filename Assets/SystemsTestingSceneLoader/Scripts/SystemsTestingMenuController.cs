using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class SystemsTestingMenuController : MonoBehaviour
{
    public static SystemsTestingMenuController instance;    //Instance of this object
    [SerializeField] string levelToLoad;    //Current level to load
    [SerializeField] int sceneIndex;    //Current scene index
    public string LevelToLoad { get => levelToLoad; set => levelToLoad = value; }   //Level to load property
    public int SceneIndex { get => sceneIndex; set => sceneIndex = value; } //Scene index property

    [SerializeField] bool isPaused; //Is the simulation paused
    [SerializeField] GameObject pausePanel; //Pause panel reference
    [SerializeField] GameObject mainMenuPanel;  //Main menu panel reference
    [SerializeField] TextMeshProUGUI levelToLoadText;   //Level to load text reference
    [SerializeField] FirstPersonController firstPersonController;   //First person controller reference. Will not always have a value, depends on scene. Turns off mouse look so it doesnt conflict
    private void Awake()
    {
        //If instance is null, set instance to this
        if(instance == null)
        {
            instance = this;
        }
        //Else if instance is not equal to this destroy the gameobject this object lives on
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        //Keep this object alive across the scenes
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {   
        //Set scene index to first scene after current scene
        SceneIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Parse out file path name to get name of level, then set level to load and level to load text to sceneName
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(SceneIndex);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
        levelToLoad = sceneName;
        levelToLoadText.text = levelToLoad;

        //If escape is pressed toggle paused, if you find a player with a first person controller, pause that mouse look component as well
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (GameObject.FindGameObjectWithTag("Player")!=null)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>() != null)
                {
                    firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
                    firstPersonController.m_MouseLook.SetCursorLock(isPaused);
                }
            }
        }


        //If ispaused, Set pause panel active, set cursor lock to none and set to visable
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //If not paused, set pause panel inactive, set cursor lock to locked and set to invisible
        else if(!isPaused)
        {
            pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //If the main menu panel is active, set cursor lock to none, and set cursor to visable
        if (mainMenuPanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //Increments scene index by one, if over sets to first scene.
    public void IncrementSceneIndex()
    {
        SceneIndex++;
        if(SceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            SceneIndex = 1;
        }
    }

    //Decrements scene index by one, if under sets to last scene.
    public void DecrementSceneIndex()
    {
        SceneIndex--;
        if (SceneIndex < 1)
        {
            SceneIndex = SceneManager.sceneCountInBuildSettings-1;
        }
    }

    //Loads leveltoload that is set by incrementing and decrementing sceneindex
    public void LoadLevelByName()
    { 
        SceneManager.LoadScene(levelToLoad);
    }

    //Loads main menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Resets current level
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
    }

    //Quits the game if in game mode
    public void QuitGame()
    {
        Application.Quit();
    }

    //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    //Once level is finised loading, check against main scene. If true, and main menu panel is not active, activate it. Set curor lockmode to none, set cursor to visable and unpause the game so we can actually use the menu.
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SystemsTestingMainMenu")
        {
            if (!mainMenuPanel.activeInHierarchy)
            {
                mainMenuPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = false;
            }
        }
    }
}
