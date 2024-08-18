// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

namespace F4B1.Core
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Movement")]
        private Vector2 input;
        [SerializeField] private InputAction moveInputAction;
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private float speed;

        [Header("Scrolling")] 
        private float scrollInput;
        [SerializeField] private InputAction onScrollAction;
        [SerializeField] private PixelPerfectCamera pixelPerfectCamera;
        [SerializeField] private Vector2 scrollBounds;
        [SerializeField] private float scrollSpeed;
        private float scrollSize;

        private void OnEnable()
        {
            onScrollAction.Enable();
            moveInputAction.Enable();
            scrollSize = pixelPerfectCamera.assetsPPU;
        }

        private void OnDisable()
        {
            onScrollAction.Disable();
            moveInputAction.Disable();
        }
        
        public void Update()
        {
            input = moveInputAction.ReadValue<Vector2>();
            scrollInput = onScrollAction.ReadValue<Vector2>().y;
            
            cameraTarget.Translate(input * (speed * Time.deltaTime));
            scrollSize += scrollInput * scrollSpeed * Time.deltaTime;
            scrollSize = Math.Clamp(scrollSize, scrollBounds.x, scrollBounds.y);
            pixelPerfectCamera.assetsPPU = Mathf.RoundToInt(scrollSize);
        }

        public void OnNavigate(InputValue value)
        {
            input = value.Get<Vector2>();
        }

        public void OnScroll(InputValue value)
        {
            scrollInput = -value.Get<Vector2>().y;
        }
    }
}