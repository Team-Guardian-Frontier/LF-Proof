using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class SceneInjector : MonoBehaviour
{
    /* A class meant to handle "lightweight" DI for scene
     * But really, mainly supplies Scene events and load order, the DI is a plus, to set up how access + dependency works.
     */


    //PLAYERS: Not in script dictionary; methods to request below
    [SerializeField] private GameObject Player1 = null;
    [SerializeField] private GameObject Player2 = null;

    //Needed Scene Objects
    private AudioManager DiskJockey = null;
    [SerializeField] private FoodSpawner foodspawn = null;
    [SerializeField] private FoodPooler foodpool = null;
    [SerializeField] private GlobalTimer Globetime = null;
    [SerializeField] private PauseFunction Pause = null;
    [SerializeField] private GameStats gamestats = null;




    //Private vars
    public bool Loaded { get; private set; }
    private InjectionDict SceneScripts; //Dictionary of injectable scripts


    void Awake()
    {
        //initialize
        SceneScripts = new InjectionDict();

        //Get GameManager scripts
        //if (DiskJockey == null) { DiskJockey = GameManager.Instance.RegisterInjection<AudioManager>(); }

        //Add to scripts to dictionary
        // SceneScripts.Add<AudioManager>(DiskJockey);
        SceneScripts.Add<FoodSpawner>(foodspawn);
        SceneScripts.Add<FoodPooler>(foodpool);
        SceneScripts.Add<GlobalTimer>(Globetime);
        SceneScripts.Add<PauseFunction>(Pause);



        //Preloads
        onGameOver += GameOverQuit;

    }

    private void Start()
    {
        //Breaks up Start event into 3 new separate events


        //NOTE: can't have any scripts that inject set themselves as inactive, else they either inject event won't trigger, or they inject vars incorrectly
        //Inject scripts into necessary components on start.
        InjectScripts();

        onFixedSceneLoad();

        SceneLoaded();



        //Set to true, since loaded
        Loaded = true;
    }

    // Update is called once per frame
    void Update()
    {

    }


    //INJECTION EVENT
    public event Action<InjectionDict> SceneJect;
    private void InjectScripts()    
    {
        if (SceneJect != null)
            SceneJect(SceneScripts);//Passes in ID of Dictionary, use dictionary to inject.
    }

    /* Example Injection (Place on your script)
     
     private void Awake() 
     { 
        localSceneInjector.SceneJect += myFunction; 
     }
     
     public void myFunction(InjectionDict ID)
     {
          LocalAudioManager = ID.Inject<AudioManager>();
     }
     
     */



    #region Preloaded event subscriptions

        //PreloadGameover
    private void GameOverQuit()
    {
        //Should load main menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    #endregion


    #region Scene Events

    /*LIST OF SUBSCRIBABLE EVENTS:
     * SceneJect
     * FixedSceneLoad
     * onSceneLoaded
     * onNextDay
     * onGameOver
     *
     * 
     */



    public event Action FixedSceneLoad;
    private void onFixedSceneLoad()
    {
        if (FixedSceneLoad!= null)
            FixedSceneLoad();
    }

    public event Action onSceneLoaded;
    private void SceneLoaded()
    {
        if (onSceneLoaded != null)
            onSceneLoaded();
    }

    public event Action onNextDay;
    //Only for bed
    public void NextDayActivated()
    {
        if (onNextDay != null)
            onNextDay();
    }

    public event Action onGameOver;
    public void GameOver()
    {
        if (onGameOver != null)
            onGameOver();
    }

    #endregion



}

#region Debug
/*
public static void PrintDebugString(string val)
{
    StreamWriter streamWrite = new StreamWriter(Application.dataPath + "/DebugOutput/DebugString.txt");
    streamWrite.Write(val);
    streamWrite.Close();
}
*/
#endregion



//Wrapper class for a dictionary of monobehaviours
[Serializable]
public class InjectionDict
{
    //Class to pass for injection.
    public Dictionary<Type, MonoBehaviour> ScriptDict;

    //Class Constructor
    public InjectionDict()
    {
        ScriptDict = new Dictionary<Type, MonoBehaviour>();
    }

    public void Add<T>(MonoBehaviour m) where T : Component
    {
        if (m.GetType() == typeof(T)) { ScriptDict.Add(typeof(T), m); }
        else Debug.Log("SM / InjDict: Error, Adding script" + m.name + " not equal to Type " + typeof(T));
    }

    //public registration, for your needs.
    public T Inject<T>() where T : Component
    {
        T send = null;
        bool compy = ScriptDict.TryGetValue(typeof(T), out MonoBehaviour temp);
        if (!compy)
        {
            Debug.Log("SM/InjDict: Couldn't find component of type = " + typeof(T).Name);
        }
        else if (temp.GetType() == typeof(T)) { send = temp as T; }
        return send;
    }
}

public interface ISceneInject
{
    void onSceneLoad();
}
