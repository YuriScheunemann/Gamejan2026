using UnityEngine;

[CreateAssetMenu(fileName = "NovoDialogo", menuName = "Dialogo/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [System.Serializable]
    public class Linha
    {
        public string autor;

        [TextArea(3, 10)]
        public string fala;

        public AudioClip audio;
    }

    public Linha[] linhas;
}
