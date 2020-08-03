using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*Straightforward and simple "Service Locator" for Persistent scripts.
     * Make sure all your persistent objects are attached to this script.
     *
     */

    private static GameManager _instance;

    //Static accessor
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null )
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    

    private void Awake()
    {
        //Singleton pattern
        if (_instance != null)
        {
            Debug.Log("GM: Destroyed an Instance of GM; " + this.GetInstanceID() + " for instance = " + _instance.GetInstanceID());
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);


        //Add Scripts to prefab
        if (AudioManager.instance == null)
        {
            AudioManager mgmt = RegisterInjection<AudioManager>();
            AudioManager.instance = mgmt;
        }
    }


    public T RegisterInjection<T>() where T : Component
    {
        T compy = gameObject.GetComponent<T>();
        if (compy == null)
        {
            Debug.Log("GM: Registered null component, added component of type = " + typeof(T).Name);
            compy = gameObject.AddComponent<T>();
        }
        return compy;
    }

}
