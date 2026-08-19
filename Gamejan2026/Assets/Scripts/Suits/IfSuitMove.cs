using System.Collections;
using UnityEngine;

public class IfSuitMove : MonoBehaviour
{
    [SerializeField] private GameObject[] Suits;
    [SerializeField] private SuitsEnum suitsEnum;
    [SerializeField] private int _indexSuit;
    public Vector3 _inicialSuitPosition;
    private float _lastSuitPosition;

    private void Start()
    {
        _inicialSuitPosition = gameObject.transform.position;
    }
    void OnMouseDown()
    {
        _lastSuitPosition = gameObject.transform.position.y;
    }
    private void OnMouseDrag()
    {
        if (gameObject.transform.position.y >= 3)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
        
        if (gameObject.transform.position.y <= -4)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4, gameObject.transform.position.z);
        
        if (gameObject.transform.position.y >= 3 || gameObject.transform.position.y <= -4)
            return;
        float newPos = gameObject.transform.position.y;
        newPos -= _lastSuitPosition;
        if (newPos == 0)
            return;
        StartCoroutine(lastPosition());
        newPos -= _lastSuitPosition;
        if (newPos == 0)
            return;

        if (gameObject.transform.position.y <= 0 && gameObject.transform.position.y != 3 && gameObject.transform.position.y != -4)
        {
            switch (suitsEnum)
            {
                case SuitsEnum.Red:
                    _indexSuit = 1;
                    StartCoroutine(MoveDown(0.08f));                  
                    break;

                case SuitsEnum.Blue:
                    _indexSuit = 3;
                    StartCoroutine(MoveDown(0.08f));                   
                    break;

                case SuitsEnum.Yellow:
                    _indexSuit = 2;
                    StartCoroutine(MoveDown(0.08f));                    
                    break;

                case SuitsEnum.Green:
                    _indexSuit = 0;
                    StartCoroutine(MoveDown(0.08f));                    
                    break;
            }
        }
        if (gameObject.transform.position.y > 0 && gameObject.transform.position.y != 3 && gameObject.transform.position.y != -4)
        {
            switch (suitsEnum)
            {
                case SuitsEnum.Red:
                    _indexSuit = 1;
                    StartCoroutine(MoveUp(-0.08f));                   
                    break;

                case SuitsEnum.Blue:
                    _indexSuit = 3;
                    StartCoroutine(MoveUp(-0.08f));                   
                    break;

                case SuitsEnum.Yellow:
                    _indexSuit = 2;
                    StartCoroutine(MoveUp(-0.08f));                   
                    break;

                case SuitsEnum.Green:
                    _indexSuit = 0;
                    StartCoroutine(MoveUp(-0.08f));                    
                    break;
            }
        }
    }

    private IEnumerator MoveDown(float newPosY)
    {
        Vector3 suitPosition = Suits[_indexSuit].transform.position;
      
        if (Suits[_indexSuit].transform.position.y <= -4)
        {
            suitPosition.y += newPosY;
            Suits[_indexSuit].transform.position = suitPosition;
            //Suits[_indexSuit].transform.position = new Vector3(Suits[_indexSuit].transform.position.x, -4, Suits[_indexSuit].transform.position.z);
            yield return null;
        }
        
        suitPosition.y -= newPosY;
        Suits[_indexSuit].transform.position = suitPosition;
    }
    private IEnumerator MoveUp(float newPosY)
    {
        Vector3 suitPosition = Suits[_indexSuit].transform.position;
        if (Suits[_indexSuit].transform.position.y >= 3)
        {
            suitPosition.y += newPosY;
            Suits[_indexSuit].transform.position = suitPosition;
            // Suits[_indexSuit].transform.position = new Vector3(Suits[_indexSuit].transform.position.x, 3, Suits[_indexSuit].transform.position.z);
            yield return null;
        }    
     
        suitPosition.y -= newPosY;
        Suits[_indexSuit].transform.position = suitPosition;
    }
    private IEnumerator lastPosition()
    {
        _lastSuitPosition = gameObject.transform.position.y;
        yield return null;
    }
}