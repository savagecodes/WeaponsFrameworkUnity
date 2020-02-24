using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SavageCodes.Frameworks.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Airburner/WeaponsFramework/CreateWeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [Header("Setup")] 
        public bool isPredicted = false;
        [Header("Basic Info")] 
        public string WeaponID;
        public float Range;
        public float FireRate;

        [Header("UI Settings")]
        public Image CrossHair;
        public Sprite HudIcon;
    }
}
