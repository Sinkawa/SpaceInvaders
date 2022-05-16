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
using TMPro;
using UnityEngine;

public class WeaponBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshName;
    [SerializeField] private TextMeshProUGUI textMeshTime;

    private Coroutine _timerUpdateCoroutine;
    
    public void OnWeaponChanged(IWeapon weapon)
    {
        textMeshName.text = weapon.GetName();
        
        if (_timerUpdateCoroutine != null)
            StopCoroutine(_timerUpdateCoroutine);

        var time = weapon.GetTimeOfAction();
        
        if (time != 0)
            _timerUpdateCoroutine = StartCoroutine(UpdateTimer(time));
        else
            textMeshTime.text = "";
    }

    private IEnumerator UpdateTimer(int remainingTime)
    {
        while (remainingTime > 0)
        {
            textMeshTime.text = $"{remainingTime} sec";
            remainingTime--;
            yield return new WaitForSeconds(1f);
        }
    }
}
