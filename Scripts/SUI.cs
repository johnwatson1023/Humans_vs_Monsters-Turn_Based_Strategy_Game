using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUI : MonoBehaviour
{
    [SerializeField] GameObject suih;
    [SerializeField] GameObject suim;


    GameManager gm;
    UnitController uc;
    CombatSystem cs;

    private void Start()
    {
        uc = FindObjectOfType<UnitController>();
        cs = FindObjectOfType<CombatSystem>();
    }

    private void Update()
    {
        if (gm.isHumanTurn)
        {
            if (uc.hit.transform.CompareTag("Human Units"))
            {
                suih.transform.position = uc.selectedUnit.transform.position;
                suih.SetActive(true);
            }
        }
        else if (!gm.isHumanTurn)
        {
            if (uc.hit.transform.CompareTag("Monster Units"))
            {
                suim.transform.position = uc.selectedUnit.transform.position;
                suim.SetActive(true);
            }
        }
        if (!cs.unitSelected)
        {
            suih.SetActive(false);
            suim.SetActive(false);
        }

    }
}
