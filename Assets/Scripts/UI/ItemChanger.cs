// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.UI
{
    public class ItemChanger : MonoBehaviour
    {
        [SerializeField] private StringVariable selectedItem;
        [SerializeField] private string item;
        
        public void SelectItem(bool selected)
        {
            selectedItem.SetValue(selected ? item : "none");
        }
    }
}