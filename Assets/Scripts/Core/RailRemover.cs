// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace F4B1.Core
{
    public class RailRemover : MonoBehaviour
    {

        [SerializeField] private float removalTime;
        private float timer;
        private bool markedForDeletion;
        [SerializeField] private Image image;
        [SerializeField] private Vector2Event removeRail;
        
        void Start()
        {
            timer = removalTime;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            image.fillAmount = timer / removalTime;
            
            if (timer > 0 || markedForDeletion) return;

            markedForDeletion = true;
            removeRail.Raise(transform.position);
            Destroy(gameObject);
        }
    }
}