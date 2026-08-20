using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        [Header("Prefab da camada")]
        public GameObject prefab;

        [Header("Velocidade")]
        public float multiplicadorVelocidade = 1f;

        [Header("Quantidade inicial")]
        public int quantidadeInicial = 3;

        [Header("Referências da cena")]
        public Transform[] referencias;

        [HideInInspector]
        public List<Transform> sprites =
            new List<Transform>();
    }

    [Header("Velocidade")]
    [SerializeField] private float velocidadeDoCenario = 1f;

    [Header("Câmera")]
    [SerializeField] private Camera cameraPrincipal;

    [Header("Camadas")]
    [SerializeField] private ParallaxLayer[] camadas;

    private float velocidadeAtual;
    private Coroutine slowdownCoroutine;

    private void Start()
    {
        velocidadeAtual =
            velocidadeDoCenario;

        if (cameraPrincipal == null)
        {
            cameraPrincipal =
                Camera.main;
        }

        InicializarCamadas();

        DesativarReferencias();
    }

    private void Update()
    {
        foreach (
            ParallaxLayer camada
            in camadas)
        {
            MoverCamada(camada);
            VerificarSpawn(camada);
            DestruirSpritesForaDaTela(
                camada
            );
        }
    }

    private void InicializarCamadas()
    {
        foreach (
            ParallaxLayer camada
            in camadas)
        {
            if (camada.prefab == null)
            {
                Debug.LogWarning(
                    "Uma camada do RiverScroller está sem Prefab."
                );

                continue;
            }

            float altura =
                ObterAltura(
                    camada.prefab
                );

            float primeiraPosicaoY =
                cameraPrincipal.transform.position.y
                -
                cameraPrincipal.orthographicSize
                -
                altura;

            for (
                int i = 0;
                i < camada.quantidadeInicial;
                i++)
            {
                Vector3 posicao =
                    new Vector3(
                        camada.prefab.transform.position.x,
                        primeiraPosicaoY +
                        (altura * i),
                        camada.prefab.transform.position.z
                    );

                GameObject novoSprite =
                    Instantiate(
                        camada.prefab,
                        posicao,
                        Quaternion.identity,
                        transform
                    );

                camada.sprites.Add(
                    novoSprite.transform
                );
            }
        }
    }

    private void MoverCamada(ParallaxLayer camada)
    {
        float velocidade =
            velocidadeAtual *
            camada.multiplicadorVelocidade;

        foreach (Transform sprite in camada.sprites)
        {
            if (sprite == null)
                continue;

            sprite.position +=
                Vector3.down *
                velocidade *
                Time.deltaTime;
        }
    }

    private void VerificarSpawn(
        ParallaxLayer camada)
    {
        if (camada.sprites.Count == 0)
            return;

        Transform spriteMaisAlto =
            ObterSpriteMaisAlto(
                camada
            );

        if (spriteMaisAlto == null)
            return;

        float altura =
            ObterAltura(
                spriteMaisAlto.gameObject
            );

        float limiteSpawn =
            cameraPrincipal.transform.position.y +
            cameraPrincipal.orthographicSize +
            altura;

        if (spriteMaisAlto.position.y <=
            limiteSpawn)
        {
            CriarNovoSprite(
                camada,
                spriteMaisAlto,
                altura
            );
        }
    }

    private void CriarNovoSprite(
        ParallaxLayer camada,
        Transform spriteMaisAlto,
        float altura)
    {
        Vector3 novaPosicao =
            new Vector3(
                camada.prefab.transform.position.x,
                spriteMaisAlto.position.y +
                altura,
                camada.prefab.transform.position.z
            );

        GameObject novoSprite =
            Instantiate(
                camada.prefab,
                novaPosicao,
                Quaternion.identity,
                transform
            );

        camada.sprites.Add(
            novoSprite.transform
        );
    }

    private void DestruirSpritesForaDaTela(
        ParallaxLayer camada)
    {
        for (
            int i = camada.sprites.Count - 1;
            i >= 0;
            i--)
        {
            Transform sprite =
                camada.sprites[i];

            if (sprite == null)
            {
                camada.sprites.RemoveAt(i);
                continue;
            }

            float altura =
                ObterAltura(
                    sprite.gameObject
                );

            float limiteInferior =
                cameraPrincipal.transform.position.y -
                cameraPrincipal.orthographicSize -
                altura;

            if (sprite.position.y <
                limiteInferior)
            {
                camada.sprites.RemoveAt(i);

                Destroy(
                    sprite.gameObject
                );
            }
        }
    }

    private void DesativarReferencias()
    {
        foreach (
            ParallaxLayer camada
            in camadas)
        {
            if (camada.referencias == null)
                continue;

            foreach (
                Transform referencia
                in camada.referencias)
            {
                if (referencia != null)
                {
                    referencia.gameObject.SetActive(
                        false
                    );
                }
            }
        }
    }

    private Transform ObterSpriteMaisAlto(
        ParallaxLayer camada)
    {
        Transform maisAlto = null;

        foreach (
            Transform sprite
            in camada.sprites)
        {
            if (sprite == null)
                continue;

            if (maisAlto == null ||
                sprite.position.y >
                maisAlto.position.y)
            {
                maisAlto = sprite;
            }
        }

        return maisAlto;
    }

    private float ObterAltura(
        GameObject objeto)
    {
        SpriteRenderer renderer =
            objeto.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            return renderer.bounds.size.y;
        }

        return 1f;
    }

    public void SlowDown(
     float multiplier,
     float duration)
    {
        if (slowdownCoroutine != null)
        {
            StopCoroutine(slowdownCoroutine);
        }

        slowdownCoroutine =
            StartCoroutine(
                SlowdownRoutine(
                    multiplier,
                    duration
                )
            );
    }

    private IEnumerator SlowdownRoutine(
        float multiplier,
        float duration)
    {
        float velocidadeOriginal =
            velocidadeAtual;

        velocidadeAtual =
            velocidadeOriginal * multiplier;

        yield return new WaitForSeconds(duration);

        velocidadeAtual =
            velocidadeOriginal;

        slowdownCoroutine = null;
    }

    public float GetSpeed()
    {
        return velocidadeAtual;
    }
}