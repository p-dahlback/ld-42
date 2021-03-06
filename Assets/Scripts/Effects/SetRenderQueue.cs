﻿/*
	SetRenderQueue.cs
 
	Sets the RenderQueue of an object's materials on Awake. This will instance
	the materials, so the script won't interfere with other renderers that
	reference the same materials.
*/

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Rendering/SetRenderQueue")]

public class SetRenderQueue : MonoBehaviour
{
    [SerializeField]
    protected int[] m_queues = new int[] { 2090 };

    protected void Awake()
    {
        Material[] materials;
        var renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            var image = GetComponent<Image>();
            materials = new Material[] { image.material };
        } else
        {
            materials = renderer.materials;
        }
        for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
        {
            materials[i].renderQueue = m_queues[i];
        }
    }
}
