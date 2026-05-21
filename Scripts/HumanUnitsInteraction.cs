using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HumanUnitsInteraction : MonoBehaviour
{
    public GameObject[] humanUnits;
    RaycastHit hit;
    public int unitPowerAttacker;
    public int unitPowerDefender;

    private void Update()
    {
        AssignUnitPower();
    }
    void AssignUnitPower()
    {
        if (gameObject.name == "Human-1 [Scouts]")
        {
            unitPowerAttacker = 1;
            unitPowerDefender = 1;
        }

        if (gameObject.name == "Human-2 [Troops]")
        {
            unitPowerAttacker = 2;
            unitPowerDefender = 2;
        }

        if (gameObject.name == "Human-3 [Tech Troops]")
        {
            unitPowerAttacker = 3;
            unitPowerDefender = 3;
        }

        if (gameObject.name == "Human-4 [Gun Truck]")
        {
            unitPowerAttacker = 4;
            unitPowerDefender = 4;
        }

        if (gameObject.name == "Human-5 [Helicopter]")
        {
            unitPowerAttacker = 5;
            unitPowerDefender = 5;
        }

        if (gameObject.name == "Human-6 [Armed Car]")
        {
            unitPowerAttacker = 6;
            unitPowerDefender = 6;
        }

        if (gameObject.name == "Human-7 [Tank]")
        {
            unitPowerAttacker = 7;
            unitPowerDefender = 7;
        }

        if (gameObject.name == "Human-8 [Cyborg Giant]")
        {
            unitPowerAttacker = 8;
            unitPowerDefender = 8;
        }

        if (gameObject.name == "Human-9 [Fighter Jet]")
        {
            unitPowerAttacker = 9;
            unitPowerDefender = 9;
        }

        if (gameObject.name == "Human-10 [Mech]")
        {
            unitPowerAttacker = 10;
            unitPowerDefender = 10;
        }
    }
}
