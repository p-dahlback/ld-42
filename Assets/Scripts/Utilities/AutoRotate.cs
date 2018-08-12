using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    public enum Axis
    {
        X,
        Y,
        Z
    }

    public float rotationSpeed = 30f;
    public Axis axis = Axis.Y;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (axis)
        {
            case Axis.X:
                transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
                break;
            case Axis.Y:
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                break;
            case Axis.Z:
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                break;
        }
    }
}
