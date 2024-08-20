// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 20.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityEngine;

namespace F4B1.UI
{
    public class Manual : MonoBehaviour
    {

        [SerializeField] private GameObject manual;
        [SerializeField] private Transform manualTransform;
        private int index;
        private int childCount;

        private void Start()
        {
            childCount = manualTransform.childCount;
        }

        public void ToggleManual()
        {
           manual.SetActive(!manual.activeSelf); 
        }

        public void Next()
        {
            manualTransform.GetChild(index).gameObject.SetActive(false);
            index = index < childCount - 1 ? index + 1 : 0;
            manualTransform.GetChild(index).gameObject.SetActive(true);
        }

        public void Previous()
        {
            manualTransform.GetChild(index).gameObject.SetActive(false);
            index = index > 0 ? index - 1 : childCount - 1;
            manualTransform.GetChild(index).gameObject.SetActive(true);
        }
        
    }
}