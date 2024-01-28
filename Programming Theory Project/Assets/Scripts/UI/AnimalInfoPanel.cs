using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalInfoPanel : MonoBehaviour
{
    [SerializeField] private Text txt_animalType, txt_animalFeed, txt_animalHappiness;
    [SerializeField] private Button caressButton;
    
    private AnimalData animalData = null;

    //ENCAPSULATION
    public AnimalData AnimalData
    { 
        set
        {
            animalData = value;
        } 
    }

    void Start()
    {
        txt_animalType.text = "";
        txt_animalFeed.text = "";
        txt_animalHappiness.text = "";
    }

    void Update()
    {
        
        txt_animalType.text = animalData.AnimalType;
        txt_animalFeed.text = animalData.FeedStatus;
        txt_animalHappiness.text = animalData.HappinessStatus;
    }
}


