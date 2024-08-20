// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace F4B1.Core
{
    public class Waggon : MonoBehaviour
    {

        [SerializeField] private Vector2 offset;
        
        [SerializeField] private TextMeshProUGUI storedText;
        [SerializeField] private TextMeshProUGUI capacityText;
        [SerializeField] private Color gold;
        [SerializeField] private int capacity = 10;
        [SerializeField] private string storedResourceId = "empty";
        [SerializeField] private int stored;

        private List<string> blockedResources = new List<string>();

        private Animator animator;
        private Animator contentAnimator;
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int X = Animator.StringToHash("x");

        [SerializeField] private Vector2 oldDirection = new Vector2(-1, -1);
        
        public string GetResourceId() => storedResourceId;
        public int GetStoredAmount() => stored;

        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            UpdateText();
            UpdateStoredResource(storedResourceId);
        }

        private void UpdateText()
        {
            storedText.text = $"{stored}"; 
            storedText.color = stored == capacity ? gold : Color.white;
            capacityText.text = $"/{capacity}";
        }
        
        private void UpdateAnimator(Vector2 direction)
        {
            animator.SetFloat(X, direction.x);
            animator.SetFloat(Y, direction.y);

            if (!contentAnimator) return;
            contentAnimator.SetFloat(X, direction.x);
            contentAnimator.SetFloat(Y, direction.y);
        }
        
        public void Move(float speed, Vector2 direction)
        {
            var pos = transform.position;
            if (direction.x == 0)
                pos.x = Mathf.RoundToInt(pos.x);
            if (direction.y == 0)
                pos.y = Mathf.RoundToInt(pos.y);
            transform.position = pos;
            transform.Translate(direction * (speed * Time.deltaTime ));
            
            UpdateAnimator(direction);
            ApplyOffset(direction);
            
            oldDirection = direction;
        }

        public void SetOldDirection(Vector2 direction)
        {
            oldDirection = direction;
            UpdateAnimator(direction);
        }
        
        private void ApplyOffset(Vector2 direction)
        {
            var pos = transform.position;
            if (direction.x == 0 && oldDirection.x != 0)
                pos.y += direction.y * offset.y;
            if (direction.y == 0 && oldDirection.y != 0)
                pos.x += direction.x * offset.x;
            transform.position = pos;
        }

        public int Fill(string resourceId, int maxAmount)
        {
            if (storedResourceId != "empty" && storedResourceId != resourceId) return 0;
            if (blockedResources.Contains(resourceId)) return 0;

            var diff = Mathf.Min(maxAmount, capacity - stored);
            stored += diff;
            UpdateText();

            if (storedResourceId != resourceId && diff != 0)
            {
                UpdateStoredResource(resourceId);
                storedResourceId = resourceId;
            }
            
            return diff;
        }

        private GameObject GetContentById(string id)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child.name == id)
                    return child;
            }

            return null;
        }

        private void UpdateStoredResource(string id)
        {
            GetContentById(storedResourceId)?.SetActive(false);
            var content = GetContentById(id);
            if (!content) return;
            
            content.SetActive(true);
            contentAnimator = null;
            if (content.TryGetComponent<Animator>(out var anim))
                contentAnimator = anim;
            storedResourceId = id;
        }
        
        public int Empty(int requestedAmount)
        {
            var amount = Mathf.Min(requestedAmount, stored);
            stored -= amount;
            UpdateText();
            if (stored <= 0)
               UpdateStoredResource("empty"); 

            return amount;
        }

        public void BlockResource(string resource, bool value)
        {
            if (value)
                blockedResources.Add(resource);
            else
                blockedResources.Remove(resource);
        }

        public void ClearWaggon()
        {
            stored = 0;
            UpdateText();
            UpdateStoredResource("empty");
        }
    }
}