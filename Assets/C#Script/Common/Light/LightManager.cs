using System;
using System.Collections.Generic;
using C_Script.Common.DataStructure;
using C_Script.Common.Model.EventCentre;
using C_Script.Common.Model.Singleton;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace C_Script.Common.Light
{
    public enum LightType
    {
        Red,
        Bule,
        White
    }

    public class LightManager : Singleton<LightManager>
    {
        public SerializedDictionary<LightType, Light2D> Light2Ds = new SerializedDictionary<LightType, Light2D>();

        public void ChangeLight(LightType originLight, LightType newLight)
        {
            Light2Ds[originLight].enabled = false;
            Light2Ds[newLight].enabled = true;
        }
    }
}
