using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI turnText;
    public Button turnEndButtonH;
    public Button turnEndButtonM;
    [SerializeField] BaseBehavior bb;
    public bool isHumanTurn = true;
    public int initiative;
    public int maxInitiative = 3;
    NeutralUnit nu;
    [SerializeField] GameObject humanBG;
    [SerializeField] GameObject monsterBG;

    [SerializeField] GameObject lightningToMonsterBase;
    [SerializeField] GameObject lightningToHumanBase;

    public bool spaceBarHeld = false;

    Vector3 towerPos = new Vector3(2, 0, 2);
    [SerializeField] GameObject camera;
    public AudioSource towerAS;
    public AudioClip zapSound;

    [SerializeField] GameObject UIH;
    [SerializeField] GameObject UIM;
    [SerializeField] TextMeshProUGUI pressESCtoPause;
    [SerializeField] GameObject pauseMenuPanel;
    public bool isPaused = false;

    [SerializeField] GameObject menu;
    public Button rulebookButton;
    [SerializeField] GameObject ruleBookpg1;
    [SerializeField] GameObject ruleBookpg2;
    public bool inRuleBook = false;
    [SerializeField] GameObject baseAttackIMG1;
    [SerializeField] GameObject baseAttackIMG2;
    [SerializeField] GameObject moveIMG;
    [SerializeField] GameObject attackIMG;
    [SerializeField] GameObject combineIMG;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject neutralUnitIMG;

    [SerializeField] TextMeshProUGUI initiativeTextH;
    [SerializeField] TextMeshProUGUI initiativeTextM;

    [SerializeField] Slider bgmVolumeSlider;
    [SerializeField] Camera main;




    private void Start()
    {
        nu = FindObjectOfType<NeutralUnit>();
        bb = FindObjectOfType<BaseBehavior>();

        initiative = maxInitiative;
    }
    void Update()
    {
        if (inRuleBook)
        {
            if (Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(1))
            {
                ruleBookpg2.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Backspace) | Input.GetMouseButtonDown(0))
            {
                ruleBookpg2.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inRuleBook = false;
                ruleBookpg1.SetActive(false);
                ruleBookpg2.SetActive(false);
                menu.SetActive(true);
                pressESCtoPause.gameObject.SetActive(true);
            }
        }

        initiativeTextH.text = initiative.ToString() + " / " + maxInitiative.ToString();
        initiativeTextM.text = initiative.ToString() + " / " + maxInitiative.ToString();


        if (isHumanTurn)
        {
            turnText.text = "Humans' Turn";
        }
        else if (!isHumanTurn)
        {
            turnText.text = "Monsters' Turn";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceBarHeld = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceBarHeld = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

         main.GetComponent<AudioSource>().volume = bgmVolumeSlider.value;
    }

    IEnumerator LighningAttack()
    {
        Vector3 direction = (towerPos - camera.transform.position).normalized;
        Ray ray = new Ray(camera.transform.position, direction);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

        if (hit.transform.gameObject.CompareTag("Human Units"))
        {
            lightningToMonsterBase.SetActive(true);
            bb.currentBaseHealthM -= 1;
            towerAS.PlayOneShot(zapSound);
        }
        
        if (hit.transform.gameObject.CompareTag("Monster Units"))
        {
            lightningToHumanBase.SetActive(true);
            bb.currentBaseHealthH -= 1;
            towerAS.PlayOneShot(zapSound);
        }
       
        yield return new WaitForSeconds(0.5f);

        lightningToMonsterBase.SetActive(false);
        lightningToHumanBase.SetActive(false);
    }

    public void ChangeTurnH()
    {
        isHumanTurn = false;
        bb.isBaseSelectedM = false;
        initiative = maxInitiative;
        nu.droneSpawnCooldown -= 1;

        StartCoroutine(LighningAttack());

        humanBG.SetActive(false);
        monsterBG.SetActive(true);
    }
    public void ChangeTurnM()
    {
        isHumanTurn = true;
        bb.isBaseSelectedH = false;
        initiative = maxInitiative;

        StartCoroutine(LighningAttack());

        monsterBG.SetActive(false);
        humanBG.SetActive(true);
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            isPaused = true;
            UIH.SetActive(false);
            UIM.SetActive(false);
            turnText.gameObject.SetActive(false);
            pressESCtoPause.text = "Press Esc to Resume";
            pauseMenuPanel.SetActive(true);
        }
        else if (isPaused)
        {
            isPaused = false;
            turnText.gameObject.SetActive(true);
            pressESCtoPause.text = "Press Esc to Open Pause Menu";
            if (isHumanTurn)
            {
                UIH.SetActive(true);
            }

            if (!isHumanTurn)
            {
                UIM.SetActive(true);
            }

            pauseMenuPanel.SetActive(false);
        }
    }

    public void LoadHvMScene(string HumanVsMonsters)
    {
        SceneManager.LoadScene(HumanVsMonsters);
    }

    public void LoadTitleScene(string TitleScene)
    {
        SceneManager.LoadScene(TitleScene);
    }

    public void OpenRulebook()
    {
        inRuleBook = true;
        ruleBookpg1.SetActive(true);
        menu.SetActive(false);
        pressESCtoPause.gameObject.SetActive(false);
        StartCoroutine(ImageSwitcher());
    }

    IEnumerator ImageSwitcher()
    {
        while (inRuleBook)
        {
            baseAttackIMG1.SetActive(false);
            baseAttackIMG2.SetActive(false);
            moveIMG.SetActive(false);
            attackIMG.SetActive(false);
            combineIMG.SetActive(false);
            cross.SetActive(false);
            neutralUnitIMG.SetActive(false);


            yield return new WaitForSeconds(2);

            baseAttackIMG1.SetActive(true);
            baseAttackIMG2.SetActive(true);
            moveIMG.SetActive(true);
            attackIMG.SetActive(true);
            combineIMG.SetActive(true);
            cross.SetActive(true);
            neutralUnitIMG.SetActive(true);

            yield return new WaitForSeconds(2);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
