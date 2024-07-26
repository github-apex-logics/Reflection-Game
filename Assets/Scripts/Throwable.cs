using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [HideInInspector] public Vector3 throwVector;
    [HideInInspector] public Rigidbody2D _rb;
    LineRenderer _lr;
    [SerializeField] float speed;
    void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _lr = this.GetComponent<LineRenderer>();

        _rb.isKinematic = true;
        _rb.useFullKinematicContacts = true;
    }
    //onmouse events possible thanks to monobehaviour + collider2d
    void OnMouseDown()
    {
        CalculateThrowVector();
        SetArrow();
    }
    void OnMouseDrag()
    {
        CalculateThrowVector();
        SetArrow();
    }
    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //doing vector2 math to ignore the z values in our distance.
        Vector2 distance = mousePos - this.transform.position;
        //dont normalize the ditance if you want the throw strength to vary
        throwVector = -distance.normalized * 100;
    }
    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, Vector3.zero);
        _lr.SetPosition(1, throwVector.normalized / 2);
        _lr.enabled = true;
    }
    void OnMouseUp()
    {
        RemoveArrow();
        Throw();
    }
    void RemoveArrow()
    {
        _lr.enabled = false;
    }
    public void Throw()
    {
        Debug.Log("Calculations = " + throwVector);
        //_rb.AddForce(throwVector);
        _rb.velocity = throwVector *speed* Time.deltaTime ;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When we register a collision, we're going to get the first point of collision
        //Then we just reflect our rigidbody about the contact normal, maintaining velocity
        ContactPoint2D hit = collision.GetContact(0);
        _rb.velocity = Vector2.Reflect(_rb.velocity, hit.normal);
    }
}