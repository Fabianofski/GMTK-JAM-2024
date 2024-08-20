// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    public class HeadQuarter : MonoBehaviour
    {
        [SerializeField] private IntVariable railCount;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.parent) return;
            var waggon = other.transform.parent.GetComponentInChildren<Waggon>();
            if (!waggon) return;

            EmptyWaggon(waggon);
        }

        private void EmptyWaggon(Waggon waggon)
        {
            var waggonResourceId = waggon.GetResourceId();
            if (waggonResourceId != "rails") return;

            var rails = waggon.Empty(waggon.GetStoredAmount());
            railCount.Add(rails); 
        }
    }
}