using UnityEngine;

public class CardLayout : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] cardLayouts layout;
    [SerializeField] float spacing;

    private int count = 0;

    private enum cardLayouts { HORIZONTAL, ARC}

    void Start()
    {
        if(layout == cardLayouts.HORIZONTAL)
        {
            for(int i = 0; i < size * 2; i++)
            {
                GameObject node = new GameObject("Node");
                node.transform.position = new Vector3(i * spacing, transform.position.y, transform.position.z);
                node.transform.SetParent(transform);
            }
        }
    }

    public void AddToLayout(Transform child)
    {
        //Even grid
        if((transform.childCount + 1) % 2 == 0)
        {

        }
        //Odd grid
        else 
        {

        }

        for (int i = transform.childCount % 2; i < transform.childCount - 1; i += 2)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                count++;
                child.SetParent(transform.GetChild(i));
            }
        }
    }

    public void RecalculateParents()
    {

    }

    public int Count
    {
        get { return count; }
    }
}

