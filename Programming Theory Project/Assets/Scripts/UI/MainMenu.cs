using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // .UI clasic UI system - .UIElements new UI Toolkit System


public class MainMenu : MonoBehaviour
{
    [SerializeField] private InputField farmNameField;
    [SerializeField] private Button startButton;

    void Start()
    {
        if(farmNameField != null)
        {
            farmNameField.text = "";
        }
        else
        {            
            Debug.LogWarning($"farm's name input field not assigned, drag and drop it in editor");
        }

        if(startButton != null)
        {
            startButton.onClick.AddListener(() => LoadFarmScene());
        }
        else
        {
            Debug.LogWarning($"start button not assigned, drag and drop it in editor");
        }
    }


    private void SaveFarmName()
    {
        string farm_name = farmNameField.text;
        // to do: save farms name to static class

        DataManager.Instance.FarmName = farm_name;
        //other stuff if needed
    }

    private void LoadFarmScene()
    {
        if (farmNameField.text == "" )
        {
            //To Do:Instantiate a warning UI
            Debug.LogWarning($"No farm name entered");
            return;
        }
        
        //get farm name and save it DataManager instance tu use during game session
        SaveFarmName();
        SceneManager.LoadScene(1);
    }
}