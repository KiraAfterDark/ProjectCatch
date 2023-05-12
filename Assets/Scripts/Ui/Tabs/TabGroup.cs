using System;
using System.Collections.Generic;
using ProjectCatch.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Ui.Tabs
{
    public class TabGroup : MonoBehaviour
    {
        [Title("Colours")]

        [SerializeField]
        private Color notSelectedColor;

        [SerializeField]
        private Color selectedColor;
    }
}
