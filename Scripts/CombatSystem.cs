using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CombatSystem : MonoBehaviour
{
    UnitController uc;
    GameManager gm;
    BaseBehavior bb;
    GoldBarH gbh;
    GoldBarM gbm;

    public bool unitSelected = false;
    public int unitPowerAttacker;
    public int unitPowerDefender;
    public int resultPower;
    public bool humanUnitSurvived;
    public bool monsterUnitSurvived;
    public Transform attackerUnit;
    public Transform defenderUnit;
    public int earningGoldValue;
    private int[] modifierValuesDefault = { 0, 0, 0, 0, 0, 0, 0, 1, 1, 2 };
    private int[] modifierValuesStrengthend = { 0, 0, 0, 0, 0, 0, 0, 1, 1, 2 };
    private int modifierA;
    private int modifierD;
    private bool isStrengthenedA = false;
    private bool isStrengthenedD = false;

    public int unitPowerBeingCombinedH;
    public int unitPowerBeingCombinedM;

    public GameObject human1;
    public GameObject human2;
    public GameObject human3;
    public GameObject human4;
    public GameObject human5;
    public GameObject human6;
    public GameObject human7;
    public GameObject human8;
    public GameObject human9;
    public GameObject human10;

    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public GameObject monster5;
    public GameObject monster6;
    public GameObject monster7;
    public GameObject monster8;
    public GameObject monster9;
    public GameObject monster10;

    public GameObject n1;
    public GameObject n2;
    public GameObject n3;

    public AudioSource gmAS;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip drawSound;
    public AudioClip trainSound;
    public AudioClip combineSound;
    public AudioClip gameOverSound;
    public AudioClip baseAttackedSound;

    [SerializeField] GameObject modifierText;
    [SerializeField] TextMeshProUGUI modifierTextA;
    [SerializeField] TextMeshProUGUI modifierTextD;

    private void Start()
    {
        uc = FindObjectOfType<UnitController>();
        gm = FindObjectOfType<GameManager>();
        bb = FindObjectOfType<BaseBehavior>();
        gbh = FindObjectOfType<GoldBarH>();
        gbm = FindObjectOfType<GoldBarM>();

    }
    private void Update()
    {
        if (!gm.isPaused)
        {
            //Combat Calculation
            if (unitSelected)
            {
                //Restrict players to only move one tile at a time (diagonal movements allowed)
                if (Mathf.Abs(uc.hit.transform.position.x - uc.selectedUnit.position.x) <= 1 && Mathf.Abs(uc.hit.transform.position.z - uc.selectedUnit.position.z) <= 1)
                {
                    //Restrict diagonal movements
                    if (Mathf.Abs(uc.hit.transform.position.x - uc.selectedUnit.position.x) + Mathf.Abs(uc.hit.transform.position.z - uc.selectedUnit.position.z) == 1)
                    {
                        if (gm.isHumanTurn)
                        {
                            if (uc.hit.transform.CompareTag("Monster Units"))
                            {
                                AssignDefenderUnitAndPowerMonster();

                                //Modifier
                                if (!isStrengthenedA)
                                {
                                    modifierA = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Attacker modifier " + modifierA);
                                    unitPowerAttacker += modifierA;
                                }

                                if (!isStrengthenedD)
                                {
                                    modifierD = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Defender modifier " + modifierD);
                                    unitPowerDefender += modifierD;
                                }
                                // ^

                                if (unitPowerAttacker == unitPowerDefender)
                                {
                                    attackerUnit.gameObject.SetActive(false);
                                    defenderUnit.gameObject.SetActive(false);

                                    resultPower = 0;
                                    gmAS.PlayOneShot(drawSound);
                                }

                                if (unitPowerAttacker != unitPowerDefender)
                                {
                                    if (unitPowerAttacker > unitPowerDefender)
                                    {
                                        humanUnitSurvived = true;
                                        resultPower = unitPowerAttacker - unitPowerDefender;
                                        gmAS.PlayOneShot(winSound);
                                    }

                                    else if (unitPowerAttacker < unitPowerDefender)
                                    {
                                        humanUnitSurvived = false;
                                        resultPower = unitPowerDefender - unitPowerAttacker;
                                        gmAS.PlayOneShot(loseSound);
                                    }
                                    ChangeUnitHumanTurn();
                                }
                                uc.suih.SetActive(false);
                                attackerUnit = null;
                                defenderUnit = null;
                                unitSelected = false;
                                uc.hasHit = false;
                                gm.initiative -= 1;
                                StartCoroutine(ModifierText());
                            }

                            if (uc.hit.transform.CompareTag("Monster Base"))
                            {
                                Debug.Log("Base Under Attack!!");

                                //Modifier
                                if (!isStrengthenedA)
                                {
                                    modifierA = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Attacker modifier " + modifierA);
                                    unitPowerAttacker += modifierA;
                                }

                                if (!isStrengthenedD)
                                {
                                    modifierD = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Defender modifier " + modifierD);
                                    unitPowerDefender += modifierD;
                                }
                                // ^

                                bb.currentBaseHealthM -= unitPowerAttacker;
                                gmAS.PlayOneShot(baseAttackedSound);
                                attackerUnit.gameObject.SetActive(false);

                                uc.suih.SetActive(false);
                                uc.selectedUnit = null;
                                attackerUnit = null;
                                defenderUnit = null;
                                unitSelected = false;
                                uc.hasHit = false;
                                gm.initiative -= 1;
                                StartCoroutine(ModifierText());
                            }

                            //Neutral Units Human Turn
                            if (uc.hit.transform.CompareTag("Neutral Units"))
                            {
                                AssignDefenderUnitAndPowerNeutral();

                                //Modifier
                                if (!isStrengthenedA)
                                {
                                    modifierA = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Attacker modifier " + modifierA);
                                    unitPowerAttacker += modifierA;
                                }

                                if (!isStrengthenedD)
                                {
                                    modifierD = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Defender modifier " + modifierD);
                                    unitPowerDefender += modifierD;
                                }
                                // ^

                                if (unitPowerAttacker == unitPowerDefender)
                                {
                                    attackerUnit.gameObject.SetActive(false);
                                    defenderUnit.gameObject.SetActive(false);

                                    resultPower = 0;
                                    gbh.currentGoldH += earningGoldValue;
                                    gmAS.PlayOneShot(drawSound);
                                }

                                if (unitPowerAttacker != unitPowerDefender)
                                {
                                    if (unitPowerAttacker > unitPowerDefender)
                                    {
                                        humanUnitSurvived = true;
                                        resultPower = unitPowerAttacker - unitPowerDefender;
                                        gmAS.PlayOneShot(winSound);
                                    }

                                    else if (unitPowerAttacker < unitPowerDefender)
                                    {
                                        humanUnitSurvived = false;
                                        resultPower = unitPowerDefender - unitPowerAttacker;
                                        gmAS.PlayOneShot(loseSound);
                                    }
                                    ChangeUnitHumanTurnAgainstNU();
                                }
                                uc.suih.SetActive(false);
                                uc.selectedUnit = null;
                                attackerUnit = null;
                                defenderUnit = null;
                                unitSelected = false;
                                uc.hasHit = false;
                                gm.initiative -= 1;
                                StartCoroutine(ModifierText());

                            }

                        }
                        else if (!gm.isHumanTurn)
                        {
                            if (uc.hit.transform.CompareTag("Human Units"))
                            {
                                AssignDefenderUnitAndPowerHuman();

                                //Modifier
                                if (!isStrengthenedA)
                                {
                                    modifierA = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Attacker modifier " + modifierA);
                                    unitPowerAttacker += modifierA;
                                }

                                if (!isStrengthenedD)
                                {
                                    modifierD = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Defender modifier " + modifierD);
                                    unitPowerDefender += modifierD;
                                }
                                // ^

                                if (unitPowerAttacker == unitPowerDefender)
                                {
                                    attackerUnit.gameObject.SetActive(false);
                                    defenderUnit.gameObject.SetActive(false);

                                    resultPower = 0;
                                    gbm.currentGoldM += earningGoldValue;
                                    gmAS.PlayOneShot(drawSound);
                                }

                                if (unitPowerAttacker != unitPowerDefender)
                                {
                                    if (unitPowerAttacker > unitPowerDefender)
                                    {
                                        monsterUnitSurvived = true;
                                        resultPower = unitPowerAttacker - unitPowerDefender;
                                        gmAS.PlayOneShot(winSound);
                                    }

                                    else if (unitPowerAttacker < unitPowerDefender)
                                    {
                                        monsterUnitSurvived = false;
                                        resultPower = unitPowerDefender - unitPowerAttacker;
                                        gmAS.PlayOneShot(loseSound);
                                    }
                                    ChangeUnitMonsterTurn();
                                }
                                uc.selectedUnit = null;
                                attackerUnit = null;
                                defenderUnit = null;
                                unitSelected = false;
                                uc.hasHit = false;
                                gm.initiative -= 1;
                                uc.suim.SetActive(false);
                                StartCoroutine(ModifierText());
                            }

                            if (uc.hit.transform.name == "Human Base")
                            {
                                Debug.Log("Base Under Attack!!");

                                //Modifier
                                if (!isStrengthenedA)
                                {
                                    modifierA = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Attacker modifier " + modifierA);
                                    unitPowerAttacker += modifierA;
                                }

                                if (!isStrengthenedD)
                                {
                                    modifierD = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Defender modifier " + modifierD);
                                    unitPowerDefender += modifierD;
                                }
                                // ^

                                bb.currentBaseHealthH -= unitPowerAttacker;
                                attackerUnit.gameObject.SetActive(false);
                                gmAS.PlayOneShot(baseAttackedSound);

                                uc.selectedUnit = null;
                                attackerUnit = null;
                                defenderUnit = null;
                                unitSelected = false;
                                uc.hasHit = false;
                                gm.initiative -= 1;
                                uc.suim.SetActive(false);
                                StartCoroutine(ModifierText());
                            }

                            //Neutral Units Monster Turn
                            if (uc.hit.transform.CompareTag("Neutral Units"))
                            {
                                AssignDefenderUnitAndPowerNeutral();

                                //Modifier
                                if (!isStrengthenedA)
                                {
                                    modifierA = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Attacker modifier " + modifierA);
                                    unitPowerAttacker += modifierA;
                                }

                                if (!isStrengthenedD)
                                {
                                    modifierD = modifierValuesDefault[Random.Range(0, modifierValuesDefault.Length)];
                                    Debug.Log("Defender modifier " + modifierD);
                                    unitPowerDefender += modifierD;
                                }
                                // ^

                                if (unitPowerAttacker == unitPowerDefender)
                                {
                                    attackerUnit.gameObject.SetActive(false);
                                    defenderUnit.gameObject.SetActive(false);

                                    resultPower = 0;
                                    gbm.currentGoldM += earningGoldValue;
                                    gmAS.PlayOneShot(drawSound);
                                }

                                if (unitPowerAttacker != unitPowerDefender)
                                {
                                    if (unitPowerAttacker > unitPowerDefender)
                                    {
                                        monsterUnitSurvived = true;
                                        resultPower = unitPowerAttacker - unitPowerDefender;
                                        gmAS.PlayOneShot(winSound);
                                    }

                                    else if (unitPowerAttacker < unitPowerDefender)
                                    {
                                        monsterUnitSurvived = false;
                                        resultPower = unitPowerDefender - unitPowerAttacker;
                                        gmAS.PlayOneShot(loseSound);
                                    }
                                    ChangeUnitMonsterTurnAgainstNU();
                                }
                                unitSelected = false;
                                Debug.Log(resultPower);
                                uc.selectedUnit = null;
                                attackerUnit = null;
                                defenderUnit = null;
                                uc.hasHit = false;
                                gm.initiative -= 1;
                                uc.suim.SetActive(false);
                                StartCoroutine(ModifierText());
                            }

                        }
                    }
                }
            }

            //Unit Combining
            if (unitSelected)
            {
                if (gm.spaceBarHeld)
                {
                    //Restrict players to only move one tile at a time (diagonal movements allowed)
                    if (Mathf.Abs(uc.hit.transform.position.x - uc.selectedUnit.position.x) <= 1 && Mathf.Abs(uc.hit.transform.position.z - uc.selectedUnit.position.z) <= 1)
                    {
                        //Restrict diagonal movements
                        if (Mathf.Abs(uc.hit.transform.position.x - uc.selectedUnit.position.x) + Mathf.Abs(uc.hit.transform.position.z - uc.selectedUnit.position.z) == 1)
                        {
                            if (gm.isHumanTurn)
                            {
                                if (uc.hit.transform.gameObject.CompareTag("Human Units"))
                                {
                                    AssignUnitBeingCombinedH();

                                    if (unitPowerAttacker + unitPowerBeingCombinedH <= 10)
                                    {
                                        resultPower = unitPowerAttacker + unitPowerBeingCombinedH;

                                        UnitCombiningHumanTurn();
                                        gmAS.PlayOneShot(combineSound);

                                        uc.suih.SetActive(false);
                                        uc.selectedUnit = null;
                                        attackerUnit = null;
                                        defenderUnit = null;
                                        unitSelected = false;
                                        uc.hasHit = false;
                                        gm.initiative -= 1;

                                    }
                                    else if (unitPowerAttacker + unitPowerBeingCombinedH > 10)
                                    {
                                        Debug.Log("Units Cannot be combined! (exceeding max power for a unit)");
                                    }
                                }
                            }

                            if (!gm.isHumanTurn)
                            {
                                if (uc.hit.transform.gameObject.CompareTag("Monster Units"))
                                {
                                    AssignUnitBeingCombinedM();

                                    if (unitPowerAttacker + unitPowerBeingCombinedH <= 10)
                                    {
                                        resultPower = unitPowerAttacker + unitPowerBeingCombinedM;

                                        UnitCombiningMonsterTurn();
                                        gmAS.PlayOneShot(combineSound);

                                        uc.suim.SetActive(false);
                                        uc.selectedUnit = null;
                                        attackerUnit = null;
                                        defenderUnit = null;
                                        unitSelected = false;
                                        uc.hasHit = false;
                                        gm.initiative -= 1;
                                    }
                                    else if (unitPowerAttacker + unitPowerBeingCombinedH > 10)
                                    {
                                        Debug.Log("Units Cannot be combined! (exceeding max power for a unit)");
                                    }
                                }
                            }
                        }
                    }

                }
            }

        }



    }

    IEnumerator ModifierText()
    {
        modifierText.SetActive(true);
        modifierTextA.text = modifierA.ToString();
        modifierTextD.text = modifierD.ToString();
        yield return new WaitForSeconds(2);
        modifierText.SetActive(false);
    }

    public void UnitCombiningHumanTurn()
    {
        if (resultPower == 2)
        {
            Instantiate(human2, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 3)
        {
            Instantiate(human3, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 4)
        {
            Instantiate(human4, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 5)
        {
            Instantiate(human5, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 6)
        {
            Instantiate(human6, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 7)
        {
            Instantiate(human7, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 8)
        {
            Instantiate(human8, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 9)
        {
            Instantiate(human9, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 10)
        {
            Instantiate(human10, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);
    }

    public void UnitCombiningMonsterTurn()
    {
        if (resultPower == 2)
        {
            Instantiate(monster2, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 3)
        {
            Instantiate(monster3, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 4)
        {
            Instantiate(monster4, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 5)
        {
            Instantiate(monster5, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 6)
        {
            Instantiate(monster6, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 7)
        {
            Instantiate(monster7, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 8)
        {
            Instantiate(monster8, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 9)
        {
            Instantiate(monster9, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);

        if (resultPower == 10)
        {
            Instantiate(monster10, uc.hit.transform.position, Quaternion.identity);
        }
        attackerUnit.gameObject.SetActive(false);
        uc.hit.transform.gameObject.SetActive(false);
    }


    public void AssignUnitBeingCombinedH()
    {
        if (uc.hit.transform.name == ("Human-1 [Scouts](Clone)"))
        {
            unitPowerBeingCombinedH = 1;
        }

        if (uc.hit.transform.name == ("Human-2 [Troops](Clone)"))
        {
            unitPowerBeingCombinedH = 2;
        }

        if (uc.hit.transform.name == ("Human-3 [Tech Troops](Clone)"))
        {
            unitPowerBeingCombinedH = 3;
        }

        if (uc.hit.transform.name == ("Human-4 [Gun Truck](Clone)"))
        {
            unitPowerBeingCombinedH = 4;
        }

        if (uc.hit.transform.name == ("Human-5 [Helicopter](Clone)"))
        {
            unitPowerBeingCombinedH = 5;
        }

        if (uc.hit.transform.name == ("Human-6 [Armed Car](Clone)"))
        {
            unitPowerBeingCombinedH = 6;
        }

        if (uc.hit.transform.name == ("Human-7 [Tank](Clone)"))
        {
            unitPowerBeingCombinedH = 7;
        }

        if (uc.hit.transform.name == ("Human-8 [Cyborg Giant](Clone)"))
        {
            unitPowerBeingCombinedH = 8;
        }

        if (uc.hit.transform.name == ("Human-9 [Fighter Jet](Clone)"))
        {
            unitPowerBeingCombinedH = 9;
        }

        if (uc.hit.transform.name == ("Human-10 [Mech](Clone)"))
        {
            unitPowerBeingCombinedH = 10;
        }
    }

    public void AssignUnitBeingCombinedM()
    {
        if (uc.hit.transform.name == ("Monster 1(Clone)"))
        {
            unitPowerBeingCombinedM = 1;
        }

        if (uc.hit.transform.name == ("Monster 2(Clone)"))
        {
            unitPowerBeingCombinedM = 2;
        }

        if (uc.hit.transform.name == ("Monster 3(Clone)"))
        {
            unitPowerBeingCombinedM = 3;
        }

        if (uc.hit.transform.name == ("Monster 4(Clone)"))
        {
            unitPowerBeingCombinedM = 4;
        }

        if (uc.hit.transform.name == ("Monster 5(Clone)"))
        {
            unitPowerBeingCombinedM = 5;
        }

        if (uc.hit.transform.name == ("Monster 6(Clone)"))
        {
            unitPowerBeingCombinedM = 6;
        }

        if (uc.hit.transform.name == ("Monster 7(Clone)"))
        {
            unitPowerBeingCombinedM = 7;
        }

        if (uc.hit.transform.name == ("Monster 8(Clone)"))
        {
            unitPowerBeingCombinedM = 8;
        }

        if (uc.hit.transform.name == ("Monster 9(Clone)"))
        {
            unitPowerBeingCombinedM = 9;
        }

        if (uc.hit.transform.name == ("Monster 10(Clone)"))
        {
            unitPowerBeingCombinedM = 10;
        }


    }

    public void ChangeUnitHumanTurn()
    {
        //Human's turn [Attacker: Humans | Defender: Monsters]
        if (resultPower == 1)
        {
            if (humanUnitSurvived) //Attacker won
            {
                Instantiate(human1, uc.hit.transform.position, Quaternion.identity); //Attacker moves to defender's pos
                uc.selectedUnit = human1.transform;
            }
            else if (!humanUnitSurvived) //Defender won
            {
                Instantiate(monster1, uc.hit.transform.position, Quaternion.identity); //Defender stays in their pos
                uc.selectedUnit = monster1.transform;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);

        }

        if (resultPower == 2)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human2, uc.hit.transform.transform.position, Quaternion.identity);
                uc.selectedUnit = human2.transform;
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster2, uc.hit.transform.position, Quaternion.identity);
                uc.selectedUnit = monster2.transform;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 3)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human3, uc.hit.transform.transform.position, Quaternion.identity);
                uc.selectedUnit = human3.transform;
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster3, uc.hit.transform.position, Quaternion.identity);
                uc.selectedUnit = monster3.transform;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 4)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human4, uc.hit.transform.transform.position, Quaternion.identity);
                uc.selectedUnit = human4.transform;
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster4, uc.hit.transform.position, Quaternion.identity);
                uc.selectedUnit = monster4.transform;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 5)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human5, uc.hit.transform.transform.position, Quaternion.identity);
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster5, uc.hit.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 6)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human6, uc.hit.transform.transform.position, Quaternion.identity);
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster6, uc.hit.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 7)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human7, uc.hit.transform.transform.position, Quaternion.identity);
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster7, uc.hit.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 8)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human8, uc.hit.transform.transform.position, Quaternion.identity);
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster8, uc.hit.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 9)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human9, uc.hit.transform.transform.position, Quaternion.identity);
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster9, uc.hit.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower >= 10)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human10, uc.hit.transform.transform.position, Quaternion.identity);
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(monster10, uc.hit.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

    }

    public void ChangeUnitMonsterTurn()
    {
        //Monster's turn [Attacker: Monsters | Defender: Humans]
        if (resultPower == 1)
        {
            if (!monsterUnitSurvived) //Defender won
            {
                Instantiate(human1, uc.hit.transform.position, Quaternion.identity); //Defender stays on their pos
            }
            else if (monsterUnitSurvived) //Attacker won
            {
                Instantiate(monster1, uc.hit.transform.transform.position, Quaternion.identity); //Attacker moves to defender's pos
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 2)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human2, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster2, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 3)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human3, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster3, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 4)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human4, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster4, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 5)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human5, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster5, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 6)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human6, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster6, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 7)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human7, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster7, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 8)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human8, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster8, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 9)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human9, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster9, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower >= 10)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(human10, uc.hit.transform.position, Quaternion.identity);
            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster10, uc.hit.transform.transform.position, Quaternion.identity);
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

    }


    public void AssignAttackerUnitAndPowerHuman()
    {
        attackerUnit = uc.selectedUnit;

        if (uc.selectedUnit.name == "Human-1 [Scouts]" | uc.selectedUnit.name == "Human-1 [Scouts](Clone)")
        {
            unitPowerAttacker = 1;
        }

        if (uc.selectedUnit.name == "Human-2 [Troops]" | uc.selectedUnit.name == "Human-2 [Troops](Clone)")
        {
            unitPowerAttacker = 2;
        }

        if (uc.selectedUnit.name == "Human-3 [Tech Troops]" | uc.selectedUnit.name == "Human-3 [Tech Troops](Clone)")
        {
            unitPowerAttacker = 3;
        }

        if (uc.selectedUnit.name == "Human-4 [Gun Truck]" | uc.selectedUnit.name == "Human-4 [Gun Truck](Clone)")
        {
            unitPowerAttacker = 4;
        }

        if (uc.selectedUnit.name == "Human-5 [Helicopter]" | uc.selectedUnit.name == "Human-5 [Helicopter](Clone)")
        {
            unitPowerAttacker = 5;
        }

        if (uc.selectedUnit.name == "Human-6 [Armed Car]" | uc.selectedUnit.name == "Human-6 [Armed Car](Clone)")
        {
            unitPowerAttacker = 6;
        }

        if (uc.selectedUnit.name == "Human-7 [Tank]" | uc.selectedUnit.name == "Human-7 [Tank](Clone)")
        {
            unitPowerAttacker = 7;
        }

        if (uc.selectedUnit.name == "Human-8 [Cyborg Giant]" | uc.selectedUnit.name == "Human-8 [Cyborg Giant](Clone)")
        {
            unitPowerAttacker = 8;
        }

        if (uc.selectedUnit.name == "Human-9 [Fighter Jet]" | uc.selectedUnit.name == "Human-9 [Fighter Jet](Clone)")
        {
            unitPowerAttacker = 9;
        }

        if (uc.selectedUnit.name == "Human-10 [Mech]" | uc.selectedUnit.name == "Human-10 [Mech](Clone)")
        {
            unitPowerAttacker = 10;
        }
    }

    public void AssignAttackerUnitAndPowerMonster()
    {
        attackerUnit = uc.selectedUnit;

        if (uc.selectedUnit.name == "Monster 1(Clone)")
        {
            unitPowerAttacker = 1;
        }

        if (uc.selectedUnit.name == "Monster 2(Clone)")
        {
            unitPowerAttacker = 2;
        }

        if (uc.selectedUnit.name == "Monster 3(Clone)")
        {
            unitPowerAttacker = 3;
        }

        if (uc.selectedUnit.name == "Monster 4(Clone)")
        {
            unitPowerAttacker = 4;
        }

        if (uc.selectedUnit.name == "Monster 5(Clone)")
        {
            unitPowerAttacker = 5;
        }

        if (uc.selectedUnit.name == "Monster 6(Clone)")
        {
            unitPowerAttacker = 6;
        }

        if (uc.selectedUnit.name == "Monster 7(Clone)")
        {
            unitPowerAttacker = 7;
        }

        if (uc.selectedUnit.name == "Monster 8(Clone)")
        {
            unitPowerAttacker = 8;
        }

        if (uc.selectedUnit.name == "Monster 9(Clone)")
        {
            unitPowerAttacker = 9;
        }

        if (uc.selectedUnit.name == "Monster 10(Clone)")
        {
            unitPowerAttacker = 10;
        }
    }

    void AssignDefenderUnitAndPowerHuman()
    {
        defenderUnit = uc.hit.transform;

        if (uc.hit.transform.name == "Human-1 [Scouts]" | uc.hit.transform.name == "Human-1 [Scouts](Clone)")
        {
            unitPowerDefender = 1;
        }

        if (uc.hit.transform.name == "Human-2 [Troops]" | uc.hit.transform.name == "Human-2 [Troops](Clone)")
        {
            unitPowerDefender = 2;
        }

        if (uc.hit.transform.name == "Human-3 [Tech Troops]" | uc.hit.transform.name == "Human-3 [Tech Troops](Clone)")
        {
            unitPowerDefender = 3;
        }

        if (uc.hit.transform.name == "Human-4 [Gun Truck]" | uc.hit.transform.name == "Human-4 [Gun Truck](Clone)")
        {
            unitPowerDefender = 4;
        }

        if (uc.hit.transform.name == "Human-5 [Helicopter]" | uc.hit.transform.name == "Human-5 [Helicopter](Clone)")
        {
            unitPowerDefender = 5;
        }

        if (uc.hit.transform.name == "Human-6 [Armed Car]" | uc.hit.transform.name == "Human-6 [Armed Car](Clone)")
        {
            unitPowerDefender = 6;
        }

        if (uc.hit.transform.name == "Human-7 [Tank]" | uc.hit.transform.name == "Human-7 [Tank](Clone)")
        {
            unitPowerDefender = 7;
        }

        if (uc.hit.transform.name == "Human-8 [Cyborg Giant]" | uc.hit.transform.name == "Human-8 [Cyborg Giant](Clone)")
        {
            unitPowerDefender = 8;
        }

        if (uc.hit.transform.name == "Human-9 [Fighter Jet]" | uc.hit.transform.name == "Human-9 [Fighter Jet](Clone)")
        {
            unitPowerDefender = 9;
        }

        if (uc.hit.transform.name == "Human-10 [Mech]" | uc.hit.transform.name == "Human-10 [Mech](Clone)")
        {
            unitPowerDefender = 10;
        }

    }

    void AssignDefenderUnitAndPowerMonster()
    {
        defenderUnit = uc.hit.transform;

        if (uc.hit.transform.name == "Monster 1(Clone)")
        {
            unitPowerDefender = 1;
        }

        if (uc.hit.transform.name == "Monster 2(Clone)")
        {
            unitPowerDefender = 2;
        }

        if (uc.hit.transform.name == "Monster 3(Clone)")
        {
            unitPowerDefender = 3;
        }

        if (uc.hit.transform.name == "Monster 4(Clone)")
        {
            unitPowerDefender = 4;
        }

        if (uc.hit.transform.name == "Monster 5(Clone)")
        {
            unitPowerDefender = 5;
        }

        if (uc.hit.transform.name == "Monster 6(Clone)")
        {
            unitPowerDefender = 6;
        }

        if (uc.hit.transform.name == "Monster 7(Clone)")
        {
            unitPowerDefender = 7;
        }

        if (uc.hit.transform.name == "Monster 8(Clone)")
        {
            unitPowerDefender = 8;
        }

        if (uc.hit.transform.name == "Monster 9(Clone)")
        {
            unitPowerDefender = 9;
        }

        if (uc.hit.transform.name == "Monster 10(Clone)")
        {
            unitPowerDefender = 10;
        }
    }

    void AssignDefenderUnitAndPowerNeutral()
    {
        defenderUnit = uc.hit.transform;

        if (uc.hit.transform.name == "N1" | uc.hit.transform.name == "N1(Clone)")
        {
            unitPowerDefender = 1;
            earningGoldValue = 2;
        }

        if (uc.hit.transform.name == "N2" | uc.hit.transform.name == "N2(Clone)")
        {
            unitPowerDefender = 2;
            earningGoldValue = 4;

        }

        if (uc.hit.transform.name == "N3" | uc.hit.transform.name == "N3(Clone)")
        {
            unitPowerDefender = 3;
            earningGoldValue = 6;

        }
    }

    public void ChangeUnitHumanTurnAgainstNU()
    {
        //Human's turn [Attacker: Humans | Defender: NU]
        if (resultPower == 1)
        {
            if (humanUnitSurvived) //Attacker won
            {
                Instantiate(human1, uc.hit.transform.position, Quaternion.identity); //Attacker moves to defender's pos
                gbh.currentGoldH += earningGoldValue;
            }
            else if (!humanUnitSurvived) //Defender won
            {
                Instantiate(n1, uc.hit.transform.position, Quaternion.identity); //Defender stays in their pos
                gbh.currentGoldH += (earningGoldValue - 2);

            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 2)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human2, uc.hit.transform.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            else if (!humanUnitSurvived)
            {
                Instantiate(n2, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += (earningGoldValue - 4);

            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 3)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human3, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 4)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human4, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 5)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human5, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 6)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human6, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 7)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human7, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 8)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human8, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 9)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human9, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower >= 10)
        {
            if (humanUnitSurvived)
            {
                Instantiate(human10, uc.hit.transform.position, Quaternion.identity);
                gbh.currentGoldH += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

    }

    public void ChangeUnitMonsterTurnAgainstNU()
    {
        Debug.Log("ChangeUnit function called");
        //Monster's turn [Attacker: Monsters | Defender: NU]
        if (resultPower == 1)
        {
            if (!monsterUnitSurvived) //Defender won
            {
                Instantiate(n1, uc.hit.transform.position, Quaternion.identity); //Defender stays on their pos
                gbm.currentGoldM += (earningGoldValue - 2);
            }
            else if (monsterUnitSurvived) //Attacker won
            {
                Instantiate(monster1, uc.hit.transform.position, Quaternion.identity); //Attacker moves to defender's pos
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 2)
        {
            if (!monsterUnitSurvived)
            {
                Instantiate(n2, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += (earningGoldValue - 4);

            }
            else if (monsterUnitSurvived)
            {
                Instantiate(monster2, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 3)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster3, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 4)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster4, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 5)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster5, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 6)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster6, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 7)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster7, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 8)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster8, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower == 9)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster9, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }

        if (resultPower >= 10)
        {
            if (monsterUnitSurvived)
            {
                Instantiate(monster10, uc.hit.transform.position, Quaternion.identity);
                gbm.currentGoldM += earningGoldValue;
            }
            attackerUnit.gameObject.SetActive(false);
            defenderUnit.gameObject.SetActive(false);
        }
        Debug.Log("ChangeUnit function finished");
    }
}
