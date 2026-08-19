using UnityEngine;

public class SuitsResetbtn : MonoBehaviour
{
    [SerializeField] private SuitsManager suitsManager;
    [SerializeField] private IfSuitMove[] ifSuitMoves;
    [SerializeField] private GameObject[] _suits;

    public void ResetSuits()
    {
        suitsManager.AllSuitsOnReach(-4);
        for(int i = 0; i< _suits.Length; i++)
        {
            _suits[i].transform.position = ifSuitMoves[i]._inicialSuitPosition;  
        }
    }
}
