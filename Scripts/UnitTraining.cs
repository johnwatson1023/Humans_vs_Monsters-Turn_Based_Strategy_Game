using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitTraining : MonoBehaviour
{
    GameManager gm;
    CombatSystem cs;
    [SerializeField] GoldBarH gbh;
    [SerializeField] GoldBarM gbm;

    [SerializeField] Button h1;
    [SerializeField] Button h2;
    [SerializeField] Button h3;
    [SerializeField] Button h4;
    [SerializeField] Button h5;
    [SerializeField] Button h6;
    [SerializeField] Button h7;
    [SerializeField] Button h8;
    [SerializeField] Button h9;
    [SerializeField] Button h10;

    [SerializeField] Button m1;
    [SerializeField] Button m2;
    [SerializeField] Button m3;
    [SerializeField] Button m4;
    [SerializeField] Button m5;
    [SerializeField] Button m6;
    [SerializeField] Button m7;
    [SerializeField] Button m8;
    [SerializeField] Button m9;
    [SerializeField] Button m10;

    [SerializeField] GameObject human1;
    [SerializeField] GameObject human2;
    [SerializeField] GameObject human3;
    [SerializeField] GameObject human4;
    [SerializeField] GameObject human5;
    [SerializeField] GameObject human6;
    [SerializeField] GameObject human7;
    [SerializeField] GameObject human8;
    [SerializeField] GameObject human9;
    [SerializeField] GameObject human10;

    [SerializeField] GameObject monster1;
    [SerializeField] GameObject monster2;
    [SerializeField] GameObject monster3;
    [SerializeField] GameObject monster4;
    [SerializeField] GameObject monster5;
    [SerializeField] GameObject monster6;
    [SerializeField] GameObject monster7;
    [SerializeField] GameObject monster8;
    [SerializeField] GameObject monster9;
    [SerializeField] GameObject monster10;

    int unitPrice;

    public Ray rayUT;
    public RaycastHit hitUT;
    public bool hasHitUT;

    [SerializeField] GameObject camera;

    //Need to fix these b/c screen point varies vary per pc
    Vector3 humanTrainPos1 = new Vector3(1, 0, 0);
    Vector3 humanTrainPos2 = new Vector3(0, 0, 1);

    Vector3 monsterTrainPos1 = new Vector3(3, 0, 4);
    Vector3 monsterTrainPos2 = new Vector3(4, 0, 3);


    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        cs = FindObjectOfType<CombatSystem>();
    }

    private void Start()
    {
        //Initial Units Spawn Human Side
        Instantiate(human1, new Vector3(2, 0, 0), Quaternion.identity);
        Instantiate(human2, new Vector3(1, 0, 1), Quaternion.identity);
        Instantiate(human3, new Vector3(0, 0, 2), Quaternion.identity);

        //Initial Units Spawn Monster Side
        Instantiate(monster1, new Vector3(2, 0, 4), Quaternion.identity);
        Instantiate(monster2, new Vector3(3, 0, 3), Quaternion.identity);
        Instantiate(monster3, new Vector3(4, 0, 2), Quaternion.identity);
    }

    public void H1()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 1;
            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human1, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human1, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }

    public void H2()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 2;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human2, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human2, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H3()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 3;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human3, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human3, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }
    }
    public void H4()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 4;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human4, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human4, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H5()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 5;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human5, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human5, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H6()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 6;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human6, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human6, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H7()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 7;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human7, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human7, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H8()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 8;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human8, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human8, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H9()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 9;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human9, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human9, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void H10()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 10;

            if (gbh.currentGoldH >= unitPrice)
            {
                Vector3 directionH1 = (humanTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionH1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionH2 = (humanTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionH2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(human10, humanTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbh.currentGoldH -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(human10, humanTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbh.currentGoldH -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbh.currentGoldH < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M1()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 1;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster1, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster1, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M2()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 2;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster2, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster2, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M3()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 3;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster3, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster3, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M4()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 4;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster4, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster4, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M5()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 5;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster5, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster5, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M6()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 6;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster6, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster6, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M7()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 7;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster7, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster7, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M8()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 8;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster8, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster8, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M9()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 9;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster9, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster9, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }
    public void M10()
    {
        if (gm.initiative > 0)
        {
            unitPrice = 10;

            if (gbm.currentGoldM >= unitPrice)
            {
                Vector3 directionM1 = (monsterTrainPos1 - camera.transform.position).normalized;
                rayUT = new Ray(camera.transform.position, directionM1);
                hasHitUT = Physics.Raycast(rayUT, out hitUT);

                if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                {
                    Vector3 directionM2 = (monsterTrainPos2 - camera.transform.position).normalized;
                    rayUT = new Ray(camera.transform.position, directionM2);
                    hasHitUT = Physics.Raycast(rayUT, out hitUT);

                    if (hitUT.transform.CompareTag("Human Units") | hitUT.transform.CompareTag("Monster Units"))
                    {
                        Debug.Log("Tiles Obstucted");
                    }

                    else
                    {
                        Instantiate(monster10, monsterTrainPos2, Quaternion.identity);
                        gm.initiative -= 1;
                        gbm.currentGoldM -= unitPrice;
                        cs.gmAS.PlayOneShot(cs.trainSound);
                    }
                }

                else
                {
                    Instantiate(monster10, monsterTrainPos1, Quaternion.identity);
                    gm.initiative -= 1;
                    gbm.currentGoldM -= unitPrice;
                    cs.gmAS.PlayOneShot(cs.trainSound);
                }
            }

            if (gbm.currentGoldM < unitPrice)
            {
                Debug.Log("Insuficient Gold");
            }

        }

        if (gm.initiative == 0)
        {
            Debug.Log("Out of initiative");
        }

    }

}
