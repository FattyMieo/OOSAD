using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
	//! Can only have selected group move and attack

    public bool isSelecting = false;
    Vector3 mousePosition1;

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if( Input.GetMouseButtonDown( 0 ) )
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

//			foreach( NPCScript selectableObject in FindObjectsOfType<NPCScript>() )
//            {
//				if( selectableObject.selectionRing != null )
//				{
//					selectableObject.selectionRing.SetActive(false);
//                }
//            }
        }
        // If we let go of the left mouse button, end selection
        if( Input.GetMouseButtonUp( 0 ) )
        {
			List<IBaseNPC> selectedObjects = new List<IBaseNPC>();
			foreach( NPCScript selectableObject in FindObjectsOfType<NPCScript>() )
            {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                {
                    selectedObjects.Add( selectableObject );
                }
            }

			NPCSelectionManagerScript.Instance.CreateGroup(selectedObjects);

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if( isSelecting )
        {
			foreach( NPCScript selectableObject in FindObjectsOfType<NPCScript>() )
            {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) )
                {
					if( !selectableObject.selectionRing.activeInHierarchy )
                    {
						selectableObject.selectionRing.SetActive( true);
//						selectableObject.selectionRing.transform.eulerAngles = new Vector3( 90, 0, 0 );
                    }
                }
                else
				{
					selectableObject.selectionRing.SetActive(false);
                }
            }
        }
    }

    public bool IsWithinSelectionBounds( GameObject gameObject )
    {
        if( !isSelecting )
            return false;

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
        return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
    }

    void OnGUI()
    {
        if( isSelecting )
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect( mousePosition1, Input.mousePosition );
            Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
            Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
        }
    }
}