// /**
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
        [SerializeField] private bool toggle;
        private Material defaultMat;

        private void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            defaultMat = spriteRenderer.material;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            spriteRenderer.material = outlineMat;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            spriteRenderer.material = defaultMat;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            toggle = !toggle;
            clickEvent.Invoke(toggle);
        }
    }
}