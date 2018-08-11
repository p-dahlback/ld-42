using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimController : AimController {

    private Vector3 oldMousePos;

    protected override void Aim()
    {
        var camera = Camera.main;
        var mousePosition = Input.mousePosition;
        if (mousePosition != oldMousePos)
        {
            oldMousePos = mousePosition;
            var targetPos = camera.ScreenToWorldPoint(mousePosition);
            Aim((Vector2)targetPos);
        }
    }
}
