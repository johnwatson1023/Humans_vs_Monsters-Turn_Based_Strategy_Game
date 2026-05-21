using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Transform selectedUnit;
    GridManager gridManager;
    GameManager gm;
    CombatSystem cs;
    public Ray ray;
    public RaycastHit hit;
    public bool hasHit;

    public Vector3 targetCords;

    public GameObject suih; //selected Unit Indicator Human
    public GameObject suim; //selected Unit Indicator Monster

    [SerializeField] float unitSpeed = 2;

    public Vector3 newCords;


    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        gm = FindObjectOfType<GameManager>();
        cs = FindObjectOfType<CombatSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (gm.initiative > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    hasHit = Physics.Raycast(ray, out hit);

                    if (hasHit)
                    {
                        if (hit.transform.CompareTag("Tile"))
                        {
                            if (cs.unitSelected)
                            {
                                targetCords = hit.transform.GetComponent<Labeller>().cords;
                                //Restrict players to only move one tile at a time (diagonal movements allowed)
                                if (Mathf.Abs(targetCords.x - selectedUnit.transform.position.x) <= 1 && Mathf.Abs(targetCords.y - selectedUnit.transform.position.z) <= 1)
                                {
                                    //Restrict diagonal movements
                                    if (Mathf.Abs(targetCords.x - selectedUnit.transform.position.x) + Mathf.Abs(targetCords.y - selectedUnit.transform.position.z) == 1)
                                    {
                                        suih.SetActive(false);
                                        suim.SetActive(false);

                                        //Moves the unit onto the new tile
                                        selectedUnit.transform.position = new Vector3(targetCords.x, selectedUnit.position.y, targetCords.y);

                                        selectedUnit = null;
                                        cs.unitSelected = false;
                                        gm.initiative -= 1;
                                    }
                                }
                            }
                        }

                        if (!gm.spaceBarHeld)
                        {
                            //Manages units movements based on turns
                            if (gm.isHumanTurn)
                            {
                                if (hit.transform.CompareTag("Human Units"))
                                {
                                    selectedUnit = hit.transform;
                                    cs.unitSelected = true;
                                    cs.AssignAttackerUnitAndPowerHuman();
                                    suih.transform.position = selectedUnit.transform.position;
                                    suih.SetActive(true);
                                }
                            }
                            else if (!gm.isHumanTurn)
                            {
                                if (hit.transform.CompareTag("Monster Units"))
                                {
                                    selectedUnit = hit.transform;
                                    cs.unitSelected = true;
                                    cs.AssignAttackerUnitAndPowerMonster();
                                    suim.transform.position = selectedUnit.transform.position;
                                    suim.SetActive(true);
                                }
                            }

                        }

                    }
                }

            }

        }
    }
}