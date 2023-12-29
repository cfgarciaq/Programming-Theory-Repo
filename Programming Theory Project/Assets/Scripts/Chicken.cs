using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        animalType = AnimalType.Chicken;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
