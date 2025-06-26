using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRenderer : MonoBehaviour
{
    void Start()
    {
        var lr = gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;
        lr.SetPosition(0, new Vector3(0, 0, 0));
        lr.SetPosition(1, new Vector3(1, 1, 0));
    }
}