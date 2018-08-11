using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAimController : AimController {
   
    protected override void Aim()
    {
        // We are going to read the input every frame
        Vector3 vNewInput = new Vector3(Input.GetAxis("RightStickX"), Input.GetAxis("RightStickY"), 0.0f);

        // Only do work if meaningful
        if (vNewInput.sqrMagnitude < 0.01f)
        {
            return;
        }

        // Apply the transform to the object  
        var angle = Mathf.Atan2(Input.GetAxis("RightStickX"), Input.GetAxis("RightStickY")) * Mathf.Rad2Deg - 90;
        Aim(angle);
    }
}
