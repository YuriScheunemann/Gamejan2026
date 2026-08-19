using System.Collections;
using UnityEngine;

public class IfSuitMove : MonoBehaviour
{
    [SerializeField] private GameObject[] Suits;
    [SerializeField] private SuitsEnum suitsEnum;
    [SerializeField] private int _indexSuit; 
    private void OnMouseDrag()
    {
        if (gameObject.transform.position.y <= 0)
        {
            switch (suitsEnum)
            {
                case SuitsEnum.Red:
                   StartCoroutine(MoveDown(0.01f));
                    _indexSuit = 1;
                    break;

                case SuitsEnum.Blue:
                    StartCoroutine(MoveDown(0.01f));
                    _indexSuit = 3;
                    break;

                case SuitsEnum.Yellow:
                    StartCoroutine(MoveDown(0.01f));
                    _indexSuit = 2;
                    break;

                case SuitsEnum.Green:
                    StartCoroutine(MoveDown(0.01f));
                    _indexSuit = 0;
                    break;
            }
        }
        if (gameObject.transform.position.y > 0)
        {
            switch (suitsEnum)
            {
                case SuitsEnum.Red:
                    StartCoroutine(MoveUp(0.01f));
                    _indexSuit = 1;
                    break;

                case SuitsEnum.Blue:
                    StartCoroutine(MoveUp(0.01f));
                    _indexSuit = 3;
                    break;

                case SuitsEnum.Yellow:
                    StartCoroutine(MoveUp(0.01f));
                    _indexSuit = 2;
                    break;

                case SuitsEnum.Green:
                    StartCoroutine(MoveUp(0.01f));
                    _indexSuit = 0;
                    break;
            }
        }
    }

    private IEnumerator MoveDown(float newPosY)
    {
        yield return new WaitForSeconds(0.25f);
        Vector3 suitPosition = Suits[_indexSuit].transform.position;
        suitPosition.y -= newPosY;
        Suits[_indexSuit].transform.position = suitPosition;       
    }
    private IEnumerator MoveUp(float newPosY)
    {
        yield return new WaitForSeconds(0.25f);
        Vector3 suitPosition = Suits[_indexSuit].transform.position;
        suitPosition.y -= newPosY;
        Suits[_indexSuit].transform.position = suitPosition;        
    }
}
