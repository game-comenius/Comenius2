﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicCursorForDoors : DynamicCursor 
{
    private static Color selectedColor = new Color(1, 1, 1, 1);
    private static Color unselectedColor = new Color(1, 1, 1, 0.4f);
    private SpriteRenderer spriteRenderer;

    protected override void OnEnable()
    {
        base.OnEnable();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void MouseIn()
    {
        base.MouseIn();

        if (spriteRenderer) spriteRenderer.color = selectedColor;
    }

    protected override void MouseOut()
    {
        base.MouseOut();

        if (spriteRenderer) spriteRenderer.color = unselectedColor;
    }
}
