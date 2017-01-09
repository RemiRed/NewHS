using UnityEngine;
using System.Collections;

public class MousePointer : MonoBehaviour {

    public Texture2D defaultMouse;
    public Texture2D pick;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
	
	void Start ()
    {
        Cursor.SetCursor(defaultMouse, hotSpot, curMode);
    }
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(pick, hotSpot, curMode);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultMouse, hotSpot, curMode);
        }
        

	
	}
  
    
 
}
