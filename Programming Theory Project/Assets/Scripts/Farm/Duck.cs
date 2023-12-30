using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        animalType = AnimalType.Duck;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
