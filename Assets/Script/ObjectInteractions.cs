using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    public Material highlightMaterial;
    
        private Material _originalMaterial;
        private GameObject _selectedObject;
    
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Check for left mouse button press
            {
                SelectObject();
            }
            else if (Input.GetMouseButtonDown(2) && _selectedObject) // Check for middle mouse button press
            {
                Destroy(_selectedObject);
            }
        }
    
        private void SelectObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (_selectedObject)
                {
                    _selectedObject.GetComponent<Renderer>().material = _originalMaterial; // Reset material of previously selected object
                }
    
                _selectedObject = hit.collider.gameObject;
                _originalMaterial = _selectedObject.GetComponent<Renderer>().material;
                _selectedObject.GetComponent<Renderer>().material = highlightMaterial;
            }
        }
}
