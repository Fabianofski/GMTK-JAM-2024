// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace F4B1.Core
{
    public class Waggon : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI storedText;
        [SerializeField] private int capacity = 10;
        [SerializeField] private string storedResourceId = "empty";
        [SerializeField] private int stored = 0; 
        
        
        public void Move(float speed, Vector2 direction)
        {
            var pos = transform.position;
            if (direction.x == 0)
                pos.x = Mathf.RoundToInt(pos.x);
            if (direction.y == 0)
                pos.y = Mathf.RoundToInt(pos.y);
            transform.position = pos;
            transform.Translate(direction * (speed * Time.deltaTime ));
        }

        public int Fill(string resourceId, int maxAmount)
        {
            if (storedResourceId != "empty" && storedResourceId != resourceId) return 0;

            var diff = Mathf.Min(maxAmount, capacity - stored);
            stored += diff;
            storedText.text = $"{stored}/{capacity}";
            
            if (storedResourceId != resourceId && diff != 0)
            {
                getWaggonById(storedResourceId)?.SetActive(false);
                getWaggonById(resourceId)?.SetActive(true);
            }
            storedResourceId = resourceId;
            
            return diff;
        }

        private GameObject getWaggonById(string id)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child.name == id)
                    return child;
            }

            return null;
        }
        
        public void Empty()
        {
        }
    }
}