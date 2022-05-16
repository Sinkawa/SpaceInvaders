// Copyright © 2022 Alexander Suvorov. All rights reserved.
// 
// This file is part of SpaceInvaders.
// 
// SpaceInvaders is free software: you can redistribute it and/or modify it under the terms of the GNU General
// Public License as published by the Free Software Foundation, either version 3 of the License, or (at your
// option) any later version.
// 
// SpaceInvaders is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the
// implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
// Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with SpaceInvaders. If not, see
// <https://www.gnu.org/licenses/>.

using System.Collections;
using UnityEngine;
public class DoubleCannon : Cannon
{
    [SerializeField] private float cannonsOffset = 1f;
    [SerializeField] private float cannonsDelay = 0.01f;
    
    public override void Shoot(Transform shotPoint)
    {
        if (!_shootingAllowed)
            return;

        StartCoroutine(ShootWithDelay(shotPoint));
        StartCoroutine(AllowShooting());
    }

    private IEnumerator ShootWithDelay(Transform shotPoint)
    {
        var shotPointPosition = shotPoint.position;
        var shotPointUp = shotPoint.up;
        
        var firstCannonPosition = shotPointPosition + cannonsOffset * 0.5f * shotPointUp;
        var secondCannonPosition = shotPointPosition - cannonsOffset * 0.5f * shotPointUp;
        
        Instantiate(projectilePrefab, firstCannonPosition, shotPoint.rotation);
        
        _shootingAllowed = false;
        yield return new WaitForSeconds(cannonsDelay); 
        Instantiate(projectilePrefab, secondCannonPosition, shotPoint.rotation);

    }
}