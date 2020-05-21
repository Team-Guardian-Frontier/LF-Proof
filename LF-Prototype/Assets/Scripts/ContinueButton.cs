using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit"))
        {
            Continue();
        }
        
    }

    public void Continue()
    {
        Debug.Log("I did it, but at what cost?");
        SceneManager.LoadScene("Menu");

        
    }

    public void Clickable()
    {
        Debug.Log("Clickable");
    }
}
