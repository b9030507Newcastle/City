using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private Color[] colors = { Color.red, Color.blue, Color.white, Color.cyan, new Color(1f, 0.5f, 0f) };

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        int randomIndex = Random.Range(0, colors.Length);
        renderer.material.color = colors[randomIndex];
    }
}
