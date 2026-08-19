using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapeCollect : MonoBehaviour
{
    [Header("Cena para carregar")]
    [SerializeField] private string nextSceneName;

    [Header("Tempo antes de trocar de cena")]
    [SerializeField] private float delay = 2f;

    private bool collected = false;

    private void OnMouseDown()
    {
        if (collected)
            return;

        collected = true;

        // Faz a fita desaparecer
        gameObject.SetActive(false);

        // Espera e troca de cena
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(nextSceneName);
    }
}