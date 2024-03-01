using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Animal;

public class AnimalInfoPanel : MonoBehaviour
{
    [SerializeField] private Text txt_animalType, txt_animalFeed, txt_animalHappiness;
    [SerializeField] private Button caressButton;

    //private AnimalData animalData = null;
    private Animal animal = null;



    //ENCAPSULATION
    public Animal Animal
    { 
        set
        {
            animal = value;
        } 
    }

    void Start()
    {
        ResetFields();
    }

    void Update()
    {        
        txt_animalType.text = animal.AnimalType;
        txt_animalFeed.text = animal.FeedStatus;
        txt_animalHappiness.text = animal.HappinessStatus;
    }

    private void ResetFields()
    {

        txt_animalType.text = "";
        txt_animalFeed.text = "";
        txt_animalHappiness.text = "";
        
        animal.IsSelected = false;
        animal = null;
    }

    public void CaressAction()
    {
        animal.Caress();
    }

    public void FeedAction()
    {
        animal.Eat();
    }

    public void ClosePanel()
    {
        ResetFields();
        gameObject.SetActive(false);
    }
}


