using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{

    private float StartPosX;
    private float StartPosY;
    Rigidbody2D rb;


    public bool isBeingHeld = false;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 pos = new Vector2(mousePos.x - StartPosX, mousePos.y - StartPosY);
            //this.gameObject.transform.localPosition = new Vector3(mousePos.x - StartPosX, mousePos.y - StartPosY, 0);
            rb.MovePosition(pos);

        }
    }


    //private void Update()
    //{
        
        
    //}


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            StartPosX = mousePos.x - this.transform.localPosition.x;
            StartPosY = mousePos.y - this.transform.localPosition.y;



            isBeingHeld = true;
        }
        

    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    private void OnMouseDrag()
    {
        if (isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - StartPosX, mousePos.y - StartPosY, 0);
        }
    }


}
