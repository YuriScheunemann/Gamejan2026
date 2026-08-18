using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BoatMoviment : MonoBehaviour
{
   private CharacterController charController;

    Rigidbody2D rb;
    Vector2 movementInput;
    [SerializeField] private float _speed = 2;
    private float _right;
    private float _left;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_left * _speed, _right * _speed);
    }

    private void Update()
    {
        _right = Input.GetAxis("Right");
        _left = Input.GetAxis("Left");
       
    }
}
