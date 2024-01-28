using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class Chicken : Animal
{
    //POLYMORPHISM
    protected override void Start()
    {
        animalType = EAnimalType.Chicken;
        base.Start();
    }

    //POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }
}
