using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColorBlindFilter : MonoBehaviour
{
    public ColorBlindSettings settings;
    private bool filterEnabled = false;
    private List<GameObject> colorBlindObjects = new List<GameObject>();

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ToggleFilter);
    }

    void ToggleFilter()
    {
        filterEnabled = !filterEnabled;
        ApplyFilter();
    }

    void ApplyFilter()
    {
        if (filterEnabled)
        {
            // Apply color blind filter settings to appropriate objects in scene
            colorBlindObjects.Clear();
            var allObjects = FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj.GetComponent<Image>() != null)
                {
                    colorBlindObjects.Add(obj);
                }
            }

            foreach (var obj in colorBlindObjects)
            {
                var image = obj.GetComponent<Image>();
                image.color = GetColorBlindColor(image.color);
            }
        }
        else
        {
            // Remove color blind filter from objects in scene
            foreach (var obj in colorBlindObjects)
            {
                var image = obj.GetComponent<Image>();
                image.color = settings.colorNormal;
            }
        }
    }

    Color GetColorBlindColor(Color color)
    {
        float r = color.r;
        float g = color.g;
        float b = color.b;

        switch (settings.filterType)
        {
            case ColorBlindType.Deuteranopia:
                r = 0.625f * r + 0.375f * g;
                g = 0.7f * r + 0.3f * g;
                break;
            case ColorBlindType.Protanopia:
                r = 0.567f * r + 0.433f * g;
                g = 0.558f * r + 0.442f * g;
                break;
            case ColorBlindType.Tritanopia:
                r = 0.95f * r + 0.05f * b;
                b = 0.433f * r + 0.567f * b;
                break;
        }

        return new Color(r, g, b, color.a);
    }
}

[System.Serializable]
public class ColorBlindSettings
{
    public Color colorNormal = Color.white;
    public ColorBlindType filterType;
}

public enum ColorBlindType
{
    Deuteranopia,
    Protanopia,
    Tritanopia
}
