using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AnimalAI))]

//ABSTRACTION
public abstract class Animal : MonoBehaviour
{
    //ENCAPSULATION
    protected enum EAnimalType { Null, Chicken, Cow, Duck, Pig, Sheep }
    protected enum EFeedStatus { Hungry, Nourished }
    protected enum EHappinessStatus { Sad, Happy }

    [SerializeField] 
    protected EAnimalType animalType = EAnimalType.Null;
    protected EFeedStatus feedStatus = EFeedStatus.Nourished;
    protected EHappinessStatus happinessStatus = EHappinessStatus.Happy;
    
    [SerializeField] 
    protected AudioClip noiseAudioClip;
    protected AudioSource audioSource;    

    [SerializeField]
    protected int maxFeedMinutes, maxHappinessMinutes;
    protected float feedTime, happinessTime, maxFeedSeconds, maxHappinessSeconds;

    protected bool isSelected = false, hasMouseOver = false;

    protected AnimalData data = new AnimalData();
    //protected Renderer renderer;

    [SerializeField]
    protected GameObject SelectionIndicator;

    #region Properties

    //ENCAPSULATION
    public string AnimalType 
    { 
        get => animalType.ToString();
    }

    public string FeedStatus
    {
        get => feedStatus.ToString();
    }

    public string HappinessStatus
    {
        get => happinessStatus.ToString();
    }

    #endregion

    #region INTERFACE METHODS

    //ENCAPSULATION USING INTERFACES
    public AnimalData GetAnimalData 
    {
        get
        {
            //UpdateAnimalData();
            return data;
        }
    }

    public bool IsSelected
    {
        set { isSelected = value; }
        get { return isSelected; }
    }    
    
    #endregion


    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //renderer = GetComponent<Renderer>();
        
        isSelected = false;
        SelectionIndicator.SetActive(isSelected);


        if (animalType != EAnimalType.Null)
        {
            MakeNoise();
        }

        StatsCalculation();
        UpdateAnimalData();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FeedingCalculation();
        HappinessCalculation();
        SelectionMarker();
    }

    protected void OnMouseEnter()
    {
        
    }

    protected void OnMouseOver()
    {
        //Activate OverMouse Effect
        //Debug.Log($"Mouse over {AnimalType}");
        hasMouseOver = true;
    }

    protected void OnMouseExit()
    {
        //Deactivate OverMouse Effect
        //Debug.Log($"Mouse exit {AnimalType}");
        hasMouseOver = false;
    }

    protected void SelectionMarker()
    {
        bool activate = isSelected || hasMouseOver;
        SelectionIndicator.SetActive(activate);
    }

    protected virtual void MakeNoise()
    {
        if(noiseAudioClip != null)
        {
            audioSource.PlayOneShot(noiseAudioClip);
        }
        else
        {
            switch (animalType)
            {
                case EAnimalType.Chicken:
                    Debug.Log($"Kokoroko!");
                    break;
                case EAnimalType.Cow:
                    Debug.Log($"Moo!");
                    break;
                case EAnimalType.Duck:
                    Debug.Log($"Quack!");
                    break;
                case EAnimalType.Pig:
                    Debug.Log($"Oink!");
                    break;
                case EAnimalType.Sheep:
                    Debug.Log($"Baa!");
                    break;
                default:
                    Debug.Log($"Awkward Silence");
                    break;
            }
        }
    }

    public virtual void Eat()
    {
        feedTime += PercentOf(maxFeedSeconds, 15);        
        feedTime = Math.Clamp(feedTime, 0, maxFeedSeconds);
        
        Debug.Log($"{animalType} ate something {feedTime}");
    }

    public void Caress()
    {
        happinessTime += PercentOf(maxHappinessSeconds, 25);
        happinessTime = Math.Clamp(happinessTime, 0, maxHappinessSeconds);

        MakeNoise();

        Debug.Log($"{animalType} likes being caressed (t_left:{happinessTime})");
    }

    protected void StatsCalculation()
    {
        maxFeedSeconds = maxFeedMinutes * 60;
        feedTime = maxFeedSeconds;
        
        maxHappinessSeconds = maxHappinessMinutes * 60;
        happinessTime = maxHappinessSeconds;        

        FeedingCalculation();
        HappinessCalculation();
    }

    protected void FeedingCalculation()
    {
        //Debug.Log($"{AnimalType} feed time: [{feedTime}]");
        
        if(feedTime > 0)
        {
            feedTime -= Time.deltaTime;
        }
        else
        {
            feedTime = 0;
        }
        
        if (feedTime <= PercentOf(maxFeedSeconds, 50f) && feedStatus == EFeedStatus.Nourished) 
        {
            feedStatus = EFeedStatus.Hungry;
            UpdateAnimalData();
        }

        if (feedTime > PercentOf(maxFeedSeconds, 50f) && feedStatus == EFeedStatus.Hungry)
        {
            feedStatus = EFeedStatus.Nourished;
            UpdateAnimalData();
        }
    }

    protected void HappinessCalculation()
    {
        //Debug.Log($"{AnimalType} Happiness time: [{happinessTime}]");
        
        if (happinessTime > 0f)
        {
            happinessTime -= Time.deltaTime;
        }
        else
        {
            happinessTime = 0;
        }

        if (happinessTime <= PercentOf(maxHappinessSeconds, 50f) && happinessStatus == EHappinessStatus.Happy)
        {
            happinessStatus = EHappinessStatus.Sad;
            UpdateAnimalData();
        }

        if (happinessTime > PercentOf(maxHappinessSeconds, 50f) && happinessStatus == EHappinessStatus.Sad)
        {
            happinessStatus = EHappinessStatus.Happy;
            UpdateAnimalData();
        }
    }

    protected float PercentOf(float value, float percent)
    {
        return (float)value * percent / 100f;
    }

    protected void UpdateAnimalData()
    {
        data.AnimalType = AnimalType;
        data.FeedStatus = FeedStatus;
        data.HappinessStatus = HappinessStatus;
    }
}


//public class to transfer animal data with other scripts like AnimalInfoPanel.cs
public class AnimalData
{
    public string AnimalType { get; set; }
    public string FeedStatus { get; set; }
    public string HappinessStatus { get; set; }
}