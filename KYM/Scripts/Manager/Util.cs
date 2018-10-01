﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    //-------------------------------------------------
    // Remap num from range 1 to range 2
    //-------------------------------------------------
    public static float RemapNumber(float num, float low1, float high1, float low2, float high2)
    {
        return low2 + (num - low1) * (high2 - low2) / (high1 - low1);
    }


    //-------------------------------------------------
    public static float RemapNumberClamped(float num, float low1, float high1, float low2, float high2)
    {
        return Mathf.Clamp(RemapNumber(num, low1, high1, low2, high2), Mathf.Min(low2, high2), Mathf.Max(low2, high2));
    }
}
