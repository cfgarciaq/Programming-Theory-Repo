using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class Animal : MonoBehaviour, IInteractable
{
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

    protected bool isSelected = false;

    protected AnimalData data = new AnimalData();
    protected Renderer renderer;

    #region Properties
    protected string AnimalType 
    { 
        get => animalType.ToString();
    }

    protected string FeedStatus
    {
        get => feedStatus.ToString();
    }

    protected string HappinessStatus
    {
        get => happinessStatus.ToString();
    }
    #endregion

    #region InterfaceMethods
    public AnimalData GetAnimalData 
    {
        get
        {
            UpdateAnimalData();
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
        renderer = GetComponent<Renderer>();
        
        if(animalType != EAnimalType.Null)
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
    }

    protected void OnMouseEnter()
    {

    }

    protected void OnMouseOver()
    {
        //Activate OverMouse Effect
        //Debug.Log($"Mouse over {AnimalType}");
    }

    protected void OnMouseExit()
    {
        //Deactivate OverMouse Effect
        //Debug.Log($"Mouse exit {AnimalType}");
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
        
        if (feedTime > maxFeedSeconds) { feedTime = maxFeedSeconds; }
        
        Debug.Log($"{animalType} is eating");
    }

    public void Caress()
    {
        happinessTime += PercentOf(maxHappinessSeconds, 25);

        MakeNoise();

        Debug.Log($"{animalType} likes being caressed");
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
        Debug.Log($"{AnimalType} feed time: [{feedTime}]");
        
        if(feedTime < 0)
        {
            return;
        }

        feedTime -= Time.deltaTime;
        
        if (feedTime <= PercentOf(maxFeedSeconds, 50f) && feedStatus == EFeedStatus.Nourished) 
        {
            feedStatus = EFeedStatus.Hungry;
        }

        if (feedTime > PercentOf(maxFeedSeconds, 50f) && feedStatus == EFeedStatus.Hungry)
        {
            feedStatus = EFeedStatus.Nourished;
        }
    }

    protected void HappinessCalculation()
    {
        Debug.Log($"{AnimalType} Happiness time: [{happinessTime}]");
        
        if (happinessTime <= 0)
        {
            return;
        }

        happinessTime -= Time.deltaTime;

        if (happinessTime <= PercentOf(maxHappinessSeconds, 50f) && happinessStatus == EHappinessStatus.Happy)
        {
            happinessStatus = EHappinessStatus.Sad;
        }

        if (happinessTime > PercentOf(maxHappinessSeconds, 50f) && happinessStatus == EHappinessStatus.Sad)
        {
            happinessStatus = EHappinessStatus.Happy;
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

public class AnimalData
{
    public string AnimalType { get; set; }
    public string FeedStatus { get; set; }
    public string HappinessStatus { get; set; }
}