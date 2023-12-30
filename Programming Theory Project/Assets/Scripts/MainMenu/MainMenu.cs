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
    }

    private void ReadTextfield()
    {
        string farm_name = farmNameField.text;
        // to do: save farms name to static class

        //DataManager.FarmName = farm_name;
        //other stuff if needed
    }

    public void LoadFarmScene()
    {
        //get farm name and save it for the play session
        SceneManager.LoadScene(1);
    }
}
