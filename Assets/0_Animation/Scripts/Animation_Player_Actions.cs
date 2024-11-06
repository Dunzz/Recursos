using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Player_Actions : MonoBehaviour
{
    public bool onItem;
    public GameObject item;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (onItem)
        {
            Debug.Log("My player on item");
            if (Input.GetKeyDown(KeyCode.E))
            {
                ActionItem();
            }
        }
    }

    public void ActionItem()
    {
        if (item != null)
        {
            anim.SetTrigger("GrabItem");
            Destroy(item);
        }
    }
}
