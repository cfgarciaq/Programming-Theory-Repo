using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class Pig : Animal
{
    //POLYMORPHISM
    protected override void Start()
    {
        animalType = EAnimalType.Pig;
        base.Start();
    }

    //POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }
}
