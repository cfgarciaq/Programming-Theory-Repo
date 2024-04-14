using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class Cow : Animal
{

    //POLYMORPHISM
    //Members values are set in Editor

    //POLYMORPHISM
    protected override void Start()
    {
        animalType = EAnimalType.Cow;
        base.Start();
    }

    //POLYMORPHISM
    protected override void Update()
    {
        base.Update();
    }
}
