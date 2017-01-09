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
        for(int i = transform.childCount - 1; i > 0; i--)
        {
            if(transform.GetChild(i).childCount == 0)
            {
                child.SetParent(transform.GetChild(i));
            }
        }

    }

    public bool Contains(Transform child)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).childCount > 0)
            {
                for(int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    if (transform.GetChild(i).GetChild(j) == child)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public int Count
    {
        get { return count; }
    }
}

