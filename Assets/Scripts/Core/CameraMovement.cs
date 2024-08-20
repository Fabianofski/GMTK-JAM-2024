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
        private Transform newTarget;
        [SerializeField] private float speed;
        [SerializeField] private Vector2 blBounds;
        [SerializeField] private Vector2 trBounds;

        [Header("Scrolling")] 
        private float scrollInput;
        [SerializeField] private InputAction onScrollAction;
        [SerializeField] private PixelPerfectCamera pixelPerfectCamera;
        [SerializeField] private CinemachineVirtualCamera cinemachine; 
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
            
            scrollSize += scrollInput * scrollSpeed * Time.deltaTime;
            scrollSize = Math.Clamp(scrollSize, scrollBounds.x, scrollBounds.y);
            pixelPerfectCamera.assetsPPU = Mathf.RoundToInt(scrollSize);

            if (newTarget && input == Vector2.zero)
                cameraTarget.position = newTarget.position;
            else if (newTarget)
            {
                cinemachine.Follow = cameraTarget;
                newTarget = null; 
            }
            else
            {
                cameraTarget.Translate(input * (speed * Time.deltaTime));
                var pos = cameraTarget.position;
                pos.x = Mathf.Clamp(pos.x, blBounds.x, trBounds.x);
                pos.y = Mathf.Clamp(pos.y, blBounds.y, trBounds.y);
                cameraTarget.position = pos;
            }
        }

        public void SetCameraTarget(GameObject target)
        {
            cinemachine.Follow = target.transform;
            newTarget = target.transform;
        }
    }
}