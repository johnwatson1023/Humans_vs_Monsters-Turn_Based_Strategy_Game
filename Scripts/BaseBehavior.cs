using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseBehavior : MonoBehaviour
{
    public Button trainingButtonH;
    public GameObject trainingButtonPadH;
    public Button trainingButtonM;
    public GameObject trainingButtonPadM;

    GameManager gm;
    CombatSystem cs;
    public bool isBaseSelectedH = false;
    public bool isBaseSelectedM = false;

    public Slider baseHealthBarH;
    public int currentBaseHealthH;
    public Slider baseHealthBarM;
    public int currentBaseHealthM;

    public GameObject gameOverScreen;
    public TextMeshProUGUI whoWonText;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        cs = FindObjectOfType<CombatSystem>();
        currentBaseHealthH = 10;
        currentBaseHealthM = 10;
    }

    // Update is called once per frame
    void Update()
    {
        baseHealthBarH.value = currentBaseHealthH;
        baseHealthBarM.value = currentBaseHealthM;

        StartCoroutine(GameOver());

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (gm.isHumanTurn)
                {
                    if (hit.transform.name == ("Human Base"))
                    {
                        Debug.Log("Human Base Clicked");
                        isBaseSelectedH = true;
                    }
                    else
                    {
                        isBaseSelectedH = false;
                    }
                }

                else if (!gm.isHumanTurn)
                {
                    if (hit.transform.name == ("Monster Base"))
                    {
                        Debug.Log("Monster Base Clicked");
                        isBaseSelectedM = true;
                    }
                    else
                    {
                        isBaseSelectedM = false;
                    }
                }
            }
        }

        if (isBaseSelectedH)
        {
            trainingButtonH.gameObject.SetActive(true);
        }
        else if (!isBaseSelectedH)
        {
            trainingButtonH.gameObject.SetActive(false);
            trainingButtonPadH.SetActive(false);
        }

        if (isBaseSelectedM)
        {
            trainingButtonM.gameObject.SetActive(true);
        }
        else if (!isBaseSelectedM)
        {
            trainingButtonM.gameObject.SetActive(false);
            trainingButtonPadM.SetActive(false);
        }

    }

    public void ToggleTrainingButtonPadH()
    {
        trainingButtonPadH.SetActive(true);
    }
    public void ToggleTrainingButtonPadM()
    {
        trainingButtonPadM.SetActive(true);
    }

    IEnumerator GameOver()
    {
        if (currentBaseHealthH <= 0)
        {
            Debug.Log("Monsters Win!!");
            yield return new WaitForSeconds(0.2f);
            cs.gmAS.PlayOneShot(cs.gameOverSound, 0.2f);
            gameOverScreen.SetActive(true);
            whoWonText.text = "Monsters Win!!";
        }

        if (currentBaseHealthM <= 0)
        {
            Debug.Log("Humans Win!!");
            yield return new WaitForSeconds(0.2f);
            cs.gmAS.PlayOneShot(cs.gameOverSound, 0.2f);
            gameOverScreen.SetActive(true);
            whoWonText.text = "Humans Win!!";
        }

    }
}
