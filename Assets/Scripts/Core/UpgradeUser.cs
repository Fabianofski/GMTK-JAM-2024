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
    public class UpgradeUser : MonoBehaviour
    {
        
        [SerializeField] private StringVariable selectedUpgrade;
        [SerializeField] private FactoryVariable selectedFactory;
        [SerializeField] private Vector2Variable mousePos;
        [SerializeField] private GameObject train;
        [SerializeField] private GameObject trainPreview;

        [SerializeField] private Vector2Variable previewPos;
        [SerializeField] private GameObjectVariable previewGo;
        
        private void Update()
        {
            if (selectedUpgrade.Value != "train") return;
            
            var roundedPos = Vector3Int.RoundToInt(mousePos.Value);
            var tilePos = new Vector3(roundedPos.x, roundedPos.y, 0);

            previewPos.SetValue(tilePos);
            previewGo.SetValue(trainPreview);
        }

        public void OnClick(bool isPressed)
        {
            if (!isPressed) return;
            if (selectedUpgrade.Value != "train") return;
            
            var roundedPos = Vector3Int.RoundToInt(mousePos.Value);
            var tilePos = new Vector3(roundedPos.x, roundedPos.y, 0);
            Instantiate(train, tilePos, Quaternion.identity);
            selectedFactory.Value.UpgradeUsed();

            selectedUpgrade.SetValue("none");
            previewGo.Reset();
        }
    }
}