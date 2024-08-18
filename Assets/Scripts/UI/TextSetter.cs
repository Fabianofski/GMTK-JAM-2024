// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityEngine;

namespace F4B1.UI
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textLabel;
        [SerializeField] private string prefix;
        [SerializeField] private string suffix;

        public void SetText(int value)
        {
            textLabel.text = $"{prefix}{value}{suffix}";
        }
    }
}