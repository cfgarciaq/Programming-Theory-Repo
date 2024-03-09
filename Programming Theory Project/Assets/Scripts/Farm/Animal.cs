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

    [SerializeField]
    protected Vector2 rngMovementDelayRange = new Vector2(0,1);
    [SerializeField]
    protected float rngMovementDistance = 5f;

    protected bool isSelected = false, hasMouseOver = false;

    protected AnimalData data = new AnimalData();
    //protected Renderer renderer;

    protected AnimalAI animalAI = null;

    [SerializeField]
    protected GameObject SelectionIndicator;

    protected float counter = 0, rngMovementDelay = 0;

    #region Properties

    //ENCAPSULATION

    public bool HasMouseOver { get { return hasMouseOver; } set { hasMouseOver = value; } }
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
        animalAI = GetComponent<AnimalAI>();
        //renderer = GetComponent<Renderer>();

        isSelected = false;
        SelectionIndicator.SetActive(isSelected);

        rngMovementDelay = GetMovementDelayRange();


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

        RandomMovement();

    }

    protected void SelectionMarker()
    {
        bool activate = isSelected || hasMouseOver;
        SelectionIndicator.SetActive(activate);
    }

    protected void RandomMovement()
    {
        if (counter < rngMovementDelay)
        {
            counter += Time.deltaTime;
            //just to limit counter min and max values
            //counter = Math.Clamp(counter, 0, rngMovementDelay + 1);
        }

        if (!isSelected && counter > rngMovementDelay)
        {
            //reset counter
            counter = 0;
            //move to random coordinate in walkablea area
            animalAI.MoveRandomly(rngMovementDistance);
            //set new movement delay to add more randomness
            rngMovementDelay = GetMovementDelayRange();
        }

    }

    protected float GetMovementDelayRange()
    {
        return UnityEngine.Random.Range(rngMovementDelayRange.x, rngMovementDelayRange.y);
    }

    protected virtual void MakeNoise()
    {
        if(noiseAudioClip != null)
        {
            audioSource.PlayOneShot(noiseAudioClip);
        }
        else
        {
            string msg = "";
            switch (animalType)
            {
                case EAnimalType.Chicken:
                    msg = "Kokoroko!";
                    break;
                case EAnimalType.Cow:
                    msg = "Moo!";
                    break;
                case EAnimalType.Duck:
                    msg = "Quack!";
                    break;
                case EAnimalType.Pig:
                    msg = "Oink!";
                    break;
                case EAnimalType.Sheep:
                    msg = "Baa!";
                    break;
                default:
                    msg = "Awkward Silence!";
                    break;
            }

            NotificationManager.Instance.CreateNotify(msg, Notification.NotificationTypes.Info);
        }
    }

    public virtual void Eat()
    {
        feedTime += PercentOf(maxFeedSeconds, 15);        
        feedTime = Math.Clamp(feedTime, 0, maxFeedSeconds);
        
        string msg = ($"{animalType} ate something {feedTime}");
        NotificationManager.Instance.CreateNotify(msg, Notification.NotificationTypes.Info);
    }

    public void Caress()
    {
        happinessTime += PercentOf(maxHappinessSeconds, 25);
        happinessTime = Math.Clamp(happinessTime, 0, maxHappinessSeconds);

        string msg = ($"{animalType} likes being caressed (t_left:{happinessTime})");
        NotificationManager.Instance.CreateNotify(msg, Notification.NotificationTypes.Info);
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
            MakeNoise();
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
            MakeNoise();

            NotificationManager.Instance.CreateNotify($"{animalType} is Sad {nameof(EHappinessStatus.Sad)}", Notification.NotificationTypes.Info);
        }

        if (happinessTime > PercentOf(maxHappinessSeconds, 50f) && happinessStatus == EHappinessStatus.Sad)
        {
            happinessStatus = EHappinessStatus.Happy;
            UpdateAnimalData();
            MakeNoise();

            NotificationManager.Instance.CreateNotify($"{animalType} is Happy {nameof(EHappinessStatus.Happy)}", Notification.NotificationTypes.Info);
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