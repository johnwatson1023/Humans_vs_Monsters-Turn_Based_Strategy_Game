using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldBarM : MonoBehaviour
{
    [SerializeField] GameObject UIH;
    [SerializeField] GameObject UIM;
    public Slider sliderM;
    public int currentGoldM;
    BaseBehavior bb;
    Vector3 minePosBR = new Vector3(4, 0, 0);
    Vector3 minePosTL = new Vector3(0, 0, 4);
    [SerializeField] GameObject camera;
    RaycastHit hitBR;
    bool hasHitBR;
    RaycastHit hitTL;
    bool hasHitTL;


    private void Start()
    {
        bb = FindObjectOfType<BaseBehavior>();
        UIM.SetActive(false);
    }

    void Update()
    {
        sliderM.value = currentGoldM;
    }
    public void EndTurnM()
    {
        if (currentGoldM < 10)
        {
            currentGoldM += 1;
        }

        //Gold Mine
        Vector3 directionBR = (minePosBR - camera.transform.position).normalized;
        Ray rayBR = new Ray(camera.transform.position, directionBR);
        hasHitBR = Physics.Raycast(rayBR, out hitBR);

        if (hitBR.transform.CompareTag("Monster Units"))
        {
            currentGoldM += 1;
        }

        Vector3 directionTL = (minePosTL - camera.transform.position).normalized;
        Ray rayTL = new Ray(camera.transform.position, directionTL);
        hasHitTL = Physics.Raycast(rayTL, out hitTL);

        if (hitTL.transform.CompareTag("Monster Units"))
        {
            currentGoldM += 1;
        }

        UIM.gameObject.SetActive(false);
        UIH.gameObject.SetActive(true);
    }

}
