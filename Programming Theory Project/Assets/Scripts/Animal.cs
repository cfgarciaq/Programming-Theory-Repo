using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Animal : MonoBehaviour
{
    protected enum AnimalType{Chicken, Cow, Duck, Pig, Sheep, Null}

    [SerializeField] protected AnimalType animalType = AnimalType.Null;
    [SerializeField] protected AudioClip noiseAudioClip;
    protected AudioSource audioSource;

    public string GetAnimalType 
    { 
        get => animalType.ToString();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(animalType != AnimalType.Null)
        {
            MakeNoise();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void MakeNoise()
    {
        audioSource.PlayOneShot(noiseAudioClip);
    }

    public virtual void Eat()
    {
        Debug.Log($"is {animalType} eating");
    }
}