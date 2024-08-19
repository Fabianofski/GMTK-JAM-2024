// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 19.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    [RequireComponent(typeof(ProductionPlant))]
    public class Factory : MonoBehaviour
    {

        private ProductionPlant plant;
        private Hoverable hoverable;
        [SerializeField] private StringVariable selectedUpgrade;
        [SerializeField] private FactoryVariable selectedFactory;
        [SerializeField] private string upgrade;

        private void Start()
        {
            plant = GetComponent<ProductionPlant>();
            hoverable = GetComponent<Hoverable>();
        }

        private void Update()
        {
            hoverable.enabled = plant.GetStoredAmount() > 0;
        }

        public void OnClick()
        {
            if (plant.GetStoredAmount() < 1) return;

            selectedUpgrade.SetValue(upgrade);
            selectedFactory.SetValue(this);
        }

        public void UpgradeUsed()
        {
            plant.ClearStorage();
            hoverable.DeactivateOutline();
        }
    }
}