using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterUnitsInteraction : MonoBehaviour
{
    public GameObject[] monsterUnits;
    RaycastHit hit;
    public int unitPowerAttackerMonster;
    public int unitPowerDefenderMonster;

    private void Start()
    {
        AssignUnitPower();
    }
    void AssignUnitPower()
    {
        if (gameObject.name == "Bug-1 (GO)")
        {
            unitPowerAttackerMonster = 1;
            unitPowerDefenderMonster = 1;
        }

        if (gameObject.name == "Bug-2 (GO)")
        {
            unitPowerAttackerMonster = 2;
            unitPowerDefenderMonster = 2;
        }

        if (gameObject.name == "Bug-3 (GO)")
        {
            unitPowerAttackerMonster = 3;
            unitPowerDefenderMonster = 3;
        }

        if (gameObject.name == "Bug-4 (GO)")
        {
            unitPowerAttackerMonster = 4;
            unitPowerDefenderMonster = 4;
        }

        if (gameObject.name == "Bug-5 (GO)")
        {
            unitPowerAttackerMonster = 5;
            unitPowerDefenderMonster = 5;
        }

        if (gameObject.name == "Bug-6 (GO)")
        {
            unitPowerAttackerMonster = 6;
            unitPowerDefenderMonster = 6;
        }

        if (gameObject.name == "Bug-7 (GO)")
        {
            unitPowerAttackerMonster = 7;
            unitPowerDefenderMonster = 7;
        }

        if (gameObject.name == "Bug-8 (GO)")
        {
            unitPowerAttackerMonster = 8;
            unitPowerDefenderMonster = 8;
        }

        if (gameObject.name == "Bug-9 (GO)")
        {
            unitPowerAttackerMonster = 9;
            unitPowerDefenderMonster = 9;
        }

        if (gameObject.name == "Bug-10 (GO)")
        {
            unitPowerAttackerMonster = 10;
            unitPowerDefenderMonster = 10;
        }
    }
}
