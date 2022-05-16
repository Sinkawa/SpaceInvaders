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

[RequireComponent(typeof(Rigidbody2D))]
public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    
    [SerializeField] private float timeToFade = 0.5f;
    [SerializeField] private float fadeDuration = 0.5f;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 movementDirection;

    private void Start()
    { 
        GetComponent<Rigidbody2D>().velocity = movementSpeed * movementDirection;
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        yield return new WaitForSeconds(timeToFade);
        var beginAlpha = textMesh.color.a;
        var alphaStep = beginAlpha / fadeDuration;

        while (textMesh.color.a > 0)
        {
            yield return new WaitForEndOfFrame();
            
            var color = textMesh.color;
            color.a -= alphaStep * Time.deltaTime;
            textMesh.color = color; 
        }
        
        Destroy(gameObject);
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
    
}
