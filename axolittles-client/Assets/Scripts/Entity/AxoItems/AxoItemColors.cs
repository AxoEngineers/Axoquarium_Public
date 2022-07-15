using System.Collections.Generic;
using UnityEngine;


public static class AxoItemColors
{
    private static Dictionary<string, Color> colorCodes = new Dictionary<string, Color>()
    {
        {"black", new Color(0 / 255.0f, 0 / 255.0f, 0 / 255.0f)},
        {"white", new Color(255f / 255.0f, 255f / 255.0f, 255f / 255.0f)},
        {"roses", new Color(243 / 255.0f, 58 / 255.0f, 106 / 255.0f)},
        {"blue", new Color(0 / 255.0f, 0 / 255.0f, 255 / 255.0f)},
        {"gold", new Color(255 / 255.0f, 215 / 255.0f, 0 / 255.0f)}
        
    };

    public static Color GetColor(string colorName) => colorCodes[colorName];
    
}
