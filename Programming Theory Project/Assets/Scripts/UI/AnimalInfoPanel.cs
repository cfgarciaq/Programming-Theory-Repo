using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalInfoPanel : MonoBehaviour
{
    [SerializeField] private Text txt_animalType, txt_animalFeed, txt_animalHappiness;
    [SerializeField] private Button caressButton;
    
    private AnimalData animalData = new AnimalData();

    // Start is called before the first frame update
    void Start()
    {
        txt_animalFeed.text = "";
        txt_animalFeed.text = "";
        txt_animalHappiness.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        txt_animalFeed.text = animalData.AnimalType;
        txt_animalFeed.text = animalData.FeedStatus;
        txt_animalHappiness.text = animalData.HappinessStatus;
    }

    public void SetAnimalData(AnimalData data)
    {
        animalData = data;
    }
}


