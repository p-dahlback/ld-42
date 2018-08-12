using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneOnly : MonoBehaviour {

    public bool hideInEditor = false;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || Application.isMobilePlatform || (Application.isEditor && hideInEditor))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
