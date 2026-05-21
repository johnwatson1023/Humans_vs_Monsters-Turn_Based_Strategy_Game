using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public Color highlightColor;
    public Color normalColor;
    private MeshCollider col;
    private MeshRenderer mr;

    private void Start()
    {
        col = GetComponent<MeshCollider>();
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (col.Raycast( ray, out hitInfo, Mathf.Infinity))
        {
            mr.material.color = highlightColor;
        }
        else
        {
            mr.material.color = normalColor;
        }
    }
}
