// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.UI
{
    public class ItemChanger : MonoBehaviour
    {
        [SerializeField] private StringVariable selectedItem;
        [SerializeField] private string item;
        private Toggle toggle;

        private void Start()
        {
            toggle = GetComponent<Toggle>();
        }

        public void SelectItem(bool selected)
        {
            selectedItem.SetValue(selected ? item : "none");
        }

        public void ItemChanged(string newItem)
        {
            toggle.isOn = newItem == item;
        }
    }
}