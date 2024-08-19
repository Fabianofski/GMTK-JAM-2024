﻿// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace F4B1.Core
{
    [RequireComponent(typeof(Collider2D))]
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        private SpriteRenderer spriteRenderer;
        [SerializeField] private Material outlineMat;
        [SerializeField] private UnityEvent<bool> clickEvent;
        [SerializeField] private UnityEvent<bool> hoverEnterEvent;
        [SerializeField] private UnityEvent<bool> hoverExitEvent;
        [SerializeField] private bool toggleable;
        private bool toggle;
        private Material defaultMat;

        private void OnEnable()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            defaultMat = spriteRenderer.material;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            spriteRenderer.material = outlineMat;
            hoverEnterEvent.Invoke(toggle);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!toggle || !toggleable) spriteRenderer.material = defaultMat;
            hoverExitEvent.Invoke(toggle);
        }

        public void DeactivateOutline()
        {
            spriteRenderer.material = defaultMat;
            toggle = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            toggle = !toggle;
            clickEvent.Invoke(toggle);
        }
    }
}