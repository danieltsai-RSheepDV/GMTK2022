using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteShape : MonoBehaviour
{
    [SerializeField] private SpriteShapeController spriteShape;
    [SerializeField] private Transform[] points;

    [Range(3,6)]
    public float currentShape = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateVerticies();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVerticies();
    }

    private void UpdateVerticies()
    {
        float numVerts = Mathf.Round(currentShape * 5f) / 5f;
        
        Spline spline = spriteShape.spline;
        spline.Clear();
        
        float angleI = (2f * Mathf.PI)/numVerts;
        for (int i = 0; i < numVerts; i++)
        {
            float angle = angleI * i;
            Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
            spline.InsertPointAt(i, offset * 5);
        }

        transform.up = new Vector2(Mathf.Sin(angleI) * -1f, Mathf.Cos(angleI));

        spriteShape.RefreshSpriteShape();
    }
}
