// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;

namespace F4B1.UI
{
    public class ToolbarExtender : MonoBehaviour
    {

        [SerializeField] private GameObject extendedToolbar;

        public void ExtendToolbar(string item)
        {
            extendedToolbar.SetActive(item == "blueprints");
        }

    }
}