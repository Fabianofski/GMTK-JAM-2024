// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 19.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace F4B1.Core
{
    public class PlayerInput : MonoBehaviour
    {

        [SerializeField] private Vector2Variable mousePos;
        [SerializeField] private BoolEvent leftClick;
        [SerializeField] private BoolEvent rightClick;

        public void OnMouseMove(InputValue value)
        {
            var pos = value.Get<Vector2>();
            mousePos.SetValue(Camera.main.ScreenToWorldPoint(pos));
        }

        public void OnClick(InputValue value)
        {
            leftClick.Raise(value.isPressed);
        }

        public void OnRightClick(InputValue value)
        {
            rightClick.Raise(value.isPressed);
        }
    }
}