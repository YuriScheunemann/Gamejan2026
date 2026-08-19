using System.Collections;
using UnityEngine;

public class IfSuitMove : MonoBehaviour
{
    [SerializeField] private GameObject[] Suits;
    [SerializeField] private SuitsEnum suitsEnum;
    [SerializeField] private int _indexSuit;
    private float _lastSuitPosition;
    void OnMouseDown()
    {
        _lastSuitPosition = gameObject.transform.position.y;
    }
    private void OnMouseDrag()
    {
        if (gameObject.transform.position.y >= 3)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 3, gameObject.transform.position.z);
        }
        if (gameObject.transform.position.y <= -4)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -4, gameObject.transform.position.z);
        }
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
        if (gameObject.transform.position.y > 0 && gameObject.transform.position.y != 3 && gameObject.transform.position.y != -4)
        {
            switch (suitsEnum)
            {
                case SuitsEnum.Red:
                    StartCoroutine(MoveUp(-0.01f));
                    _indexSuit = 1;
                    break;

                case SuitsEnum.Blue:
                    StartCoroutine(MoveUp(-0.01f));
                    _indexSuit = 3;
                    break;

                case SuitsEnum.Yellow:
                    StartCoroutine(MoveUp(-0.01f));
                    _indexSuit = 2;
                    break;

                case SuitsEnum.Green:
                    StartCoroutine(MoveUp(-0.01f));
                    _indexSuit = 0;
                    break;
            }
        }
    }

    private IEnumerator MoveDown(float newPosY)
    {
        yield return new WaitForSeconds(0.25f);
        if (Suits[_indexSuit].transform.position.y >= 3)
        {
            Suits[_indexSuit].transform.position = new Vector3(Suits[_indexSuit].transform.position.x, 3, Suits[_indexSuit].transform.position.z);
            yield return null;
        }
        if (Suits[_indexSuit].transform.position.y <= -4)
        {
            Suits[_indexSuit].transform.position = new Vector3(Suits[_indexSuit].transform.position.x, -4, Suits[_indexSuit].transform.position.z);
            yield return null;
        }
        Vector3 suitPosition = Suits[_indexSuit].transform.position;
        suitPosition.y -= newPosY - 0.1f;
        Suits[_indexSuit].transform.position = suitPosition;
    }
    private IEnumerator MoveUp(float newPosY)
    {

        yield return new WaitForSeconds(0.25f);
        if (Suits[_indexSuit].transform.position.y >= 3)
        {
            Suits[_indexSuit].transform.position = new Vector3(Suits[_indexSuit].transform.position.x, 3, Suits[_indexSuit].transform.position.z);
            yield return null;
        }
        if (Suits[_indexSuit].transform.position.y <= -4)
        {
            Suits[_indexSuit].transform.position = new Vector3(Suits[_indexSuit].transform.position.x, -4, Suits[_indexSuit].transform.position.z);
            yield return null;
        }
        Vector3 suitPosition = Suits[_indexSuit].transform.position;
        suitPosition.y -= newPosY + 0.1f;
        Suits[_indexSuit].transform.position = suitPosition;
    }
    private IEnumerator lastPosition()
    {
        _lastSuitPosition = gameObject.transform.position.y;
        yield return null;
    }
}