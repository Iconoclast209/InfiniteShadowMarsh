using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearTrain : MonoBehaviour {

    [Tooltip("Gear train to set")]
    [SerializeField]
    private Gear[] gearTrain;

    [Tooltip("Speed to start with.")]
    [SerializeField]
    private float startingSpeed;

    [Tooltip("Direction to start in.")]
    [SerializeField]
    private bool startClockwise;


    private void Start ()
    {
        SetStartingSpeed();
        if (gearTrain.Length > 1)
            print(gearTrain.Length);
            DetermineSpeedRatios(1);
	}

    private void SetStartingSpeed()
    {
        gearTrain[0].ClockwiseRotation = startClockwise;
        gearTrain[0].Speed = startingSpeed;
    }

    private void DetermineSpeedRatios(int arrayElement)
    {
        float speedRatio = gearTrain[arrayElement].TeethOnGear / gearTrain[arrayElement - 1].TeethOnGear;
        gearTrain[arrayElement].ClockwiseRotation = !gearTrain[arrayElement - 1].ClockwiseRotation;
        gearTrain[arrayElement].Speed = gearTrain[arrayElement - 1].Speed * speedRatio;
        print("Gear " + arrayElement + " speed: " + gearTrain[arrayElement].Speed);

        if (gearTrain.Length - 1 > arrayElement)
            DetermineSpeedRatios(arrayElement + 1);
        return;
    }
}
