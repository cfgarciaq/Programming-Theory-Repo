using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class Sheep : Animal
{
    //POLYMORPHISM
    //Members values are set in Editor

    //POLYMORPHISM
    protected override void Start()
    {
        animalType = EAnimalType.Sheep;
        base.Start();
    }

    //POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }
}
