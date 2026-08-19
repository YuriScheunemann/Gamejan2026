using UnityEngine;

public class RiverScroller : MonoBehaviour
{
    public float velocidadeDoCenario;
    // Update is called once per frame
    void Update()
    {
        MovimentarCenario();
    }

    private void MovimentarCenario()
    {
        Vector2 deslocamento = new Vector2(0, Time.time * velocidadeDoCenario);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }
}