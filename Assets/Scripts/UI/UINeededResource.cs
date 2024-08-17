// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.UI
{
    public class UINeededResource : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI storedText;
        [SerializeField] private Image icon;
        [SerializeField] private Sprite[] resourceIcons; 

        public void UpdateNeededResourceUI(int amount, int capacity, string resourceId)
        {
            storedText.text = $"{amount}/{capacity}";
            icon.sprite = FindSpriteByResourceId(resourceId);
        }

        private Sprite FindSpriteByResourceId(string resourceId)
        {
            foreach (var sprite in resourceIcons)
                if (sprite.name == resourceId)
                    return sprite;
            return null;
        }
    }
}