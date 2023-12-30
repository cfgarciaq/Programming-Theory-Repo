using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        animalType = AnimalType.Cow;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
