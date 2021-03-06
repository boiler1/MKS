﻿using System;
using System.Collections.Generic;
using System.Text;
using USITools;

namespace KolonyTools
{
    public class ModuleEfficiencyPart : ModuleResourceConverter_USI
    {
        [KSPField]
        public double eMultiplier = 1d;
        
        [KSPField]
        public string eTag = "";

        public float Governor = 1.0f;

        public override string GetInfo()
        {
            if (string.IsNullOrEmpty(eTag))
                return string.Empty;
            return "Boosts efficiency of converters benefiting from a " + eTag + "\n\n" +
                "Boost power: " + eMultiplier.ToString();
        }

        private double _curMult;

        public double EfficiencyMultiplier
        {
            get
            {
                if (HighLogic.LoadedSceneIsEditor)
                    return eMultiplier * Governor;
                if (!IsActivated)
                    _curMult = 0d;
                return _curMult * Governor;
            }
            set
            {
                _curMult = value;
            }
        }

        protected override void PostProcess(ConverterResults result, double deltaTime)
        {
            base.PostProcess(result,deltaTime);
            EfficiencyMultiplier = result.TimeFactor/deltaTime;
        }
    }
}

