// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;

namespace F4B1.Core
{
    public class Waggon : MonoBehaviour
    {
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
    }
}