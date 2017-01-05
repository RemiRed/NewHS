using UnityEngine;
using System.Collections;

public class CardMarker : MonoBehaviour
{
    public GameObject cards;
    public Vector3 originalPosition;
    public float previewYvalue;
    private Vector3 newLocation;
    private float mousePositionZ = 12;

  
        void OnMouseEnter() // Sets the card in a more readable position
    {
        originalPosition = transform.position;
        Vector3 previewPosition = transform.position;
        previewPosition.y += previewYvalue;

        transform.position = previewPosition;
    }

    void OnMouseExit() // Sets the card in its original position
    {
        transform.position = originalPosition;
    }

    void OnMouseDrag() // Drag the card of your choice to the gameplan
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mousePositionZ);
        Vector3 cardPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = cardPosition;
    }
}
