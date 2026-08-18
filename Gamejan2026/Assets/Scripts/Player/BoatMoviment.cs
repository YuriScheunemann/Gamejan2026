using UnityEngine;
using UnityEngine.SceneManagement;
public class BoatMoviment : MonoBehaviour
{
    Rigidbody2D rb;    
    private float _speed = 5;
    private float _moviment;
    [SerializeField] private string _sceneName;
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
        if(_speed <= 1)
            SceneManager.LoadScene(_sceneName);
    }

}
