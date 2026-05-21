using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldBarH : MonoBehaviour
{
    [SerializeField] GameObject UIH;
    [SerializeField] GameObject UIM;
    GameManager gm;
    BaseBehavior bb;
    int unitValue;
    public Slider sliderH;
    public int currentGoldH;
    public bool isStandingOnMineTopLeft = false;
    public bool isStandingOnMineButtomRight = false;
    Vector3 minePosBR = new Vector3(4, 0, 0);
    Vector3 minePosTL = new Vector3(0, 0, 4);
    [SerializeField] GameObject camera;
    RaycastHit hitBR;
    bool hasHitBR;
    RaycastHit hitTL;
    bool hasHitTL;



    private void Start()
    {
        gm = gameObject.GetComponent<GameManager>();
        bb = FindObjectOfType<BaseBehavior>();
    }

    private void Update()
    {
        sliderH.value = currentGoldH;

    }

    public void EndTurnH()
    {
        if (currentGoldH < 10)
        {
            currentGoldH += 1;
        }

        //Gold Mine
        Vector3 directionBR = (minePosBR - camera.transform.position).normalized;
        Ray rayBR = new Ray(camera.transform.position, directionBR);
        hasHitBR = Physics.Raycast(rayBR, out hitBR);

        if (hitBR.transform.CompareTag("Human Units"))
        {
            currentGoldH += 1;
        }

        Vector3 directionTL = (minePosTL - camera.transform.position).normalized;
        Ray rayTL = new Ray(camera.transform.position, directionTL);
        hasHitTL = Physics.Raycast(rayTL, out hitTL);

        if (hitTL.transform.CompareTag("Human Units"))
        {
            currentGoldH += 1;
        }

        UIH.gameObject.SetActive(false);
        UIM.gameObject.SetActive(true);
    }
}
