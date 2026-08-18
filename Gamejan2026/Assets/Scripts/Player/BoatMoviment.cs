using UnityEngine;

public class BoatMoviment : MonoBehaviour
{
    Rigidbody2D rb;    
    private float _speed = 5;
    private float _moviment;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moviment * _speed,0);
    }

    private void Update()
    {
        _moviment = Input.GetAxis("Horizontal");       
    }
    public void SlowSpeed(float slowSpeed)
    {
        _speed -= slowSpeed;
        print("slow" +  slowSpeed);
        print(_speed);
    }

}
