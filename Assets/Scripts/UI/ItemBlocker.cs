// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 20.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using F4B1.Core;
using UnityEngine;

namespace F4B1.UI
{
    public class ItemBlocker : MonoBehaviour
    {

        [SerializeField] private Waggon waggon;
        [SerializeField] private string resource;

        public void BlockResource(bool value)
        {
            waggon.BlockResource(resource, value);
        }
    }
}