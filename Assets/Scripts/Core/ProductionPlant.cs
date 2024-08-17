// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using TMPro;
using UnityEngine;

namespace F4B1.Core
{
    public class ProductionPlant: MonoBehaviour
    {
        [SerializeField] private int stored = 20;
        [SerializeField] private int capacity = 30;
        [SerializeField] private string resourceId;
        [SerializeField] private TextMeshProUGUI capacityText;

        private void Start()
        {
            capacityText.text = $"{stored}/{capacity}";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var waggon = other.GetComponentInChildren<Waggon>();
            if (!waggon) return;
            
            stored -= waggon.Fill(resourceId, stored);
            capacityText.text = $"{stored}/{capacity}";
        }
    }
}