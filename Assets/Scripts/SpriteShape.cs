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
        Spline spline = spriteShape.spline;
        spline.Clear();
        
        float offsetAngle = -0.25f * Mathf.Sin((currentShape - 0.5f) * Mathf.PI) + 0.25f;
        float angleI = (2f * Mathf.PI)/currentShape;
        for (int i = 0; i < currentShape; i++)
        {
            float angle = angleI * (i + offsetAngle);
            Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
            spline.InsertPointAt(i, offset * 5);
        }

        spriteShape.RefreshSpriteShape();
    }
}
