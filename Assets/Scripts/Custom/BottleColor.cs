using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BottleColor {
    public Color Color {
        get => color;
        set => color = value;
    }
    public int Count {
        get => count;
        set => count = value;
    }

    [SerializeField] private Color color;
    [SerializeField] private int count;
}