// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 20.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    public class PreviewRenderer : MonoBehaviour
    {
        [SerializeField] private Vector2Variable previewPos;
        [SerializeField] private GameObjectVariable previewGo;
        [SerializeField] private BoolVariable previewValid;
        private GameObject spawnedPreview;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!previewGo.Value)
            {
                if (spawnedPreview) Destroy(spawnedPreview);
                spriteRenderer.enabled = false;
                return;
            }

            if (!spawnedPreview)
            {
                spawnedPreview = Instantiate(previewGo.Value, previewPos.Value, Quaternion.identity);
                spawnedPreview.SendMessage("PreviewMode"); 
            }

            spriteRenderer.enabled = !previewValid.Value;
            transform.position = previewPos.Value;
            spawnedPreview.transform.position = previewPos.Value;
        }
    }
}