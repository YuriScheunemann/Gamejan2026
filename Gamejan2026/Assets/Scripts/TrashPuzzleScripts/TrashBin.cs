using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType acceptedType;

    // Recebe o item — decide se acerta ou erra e delega feedback.
    public void ReceiveTrash(TrashItem trash)
    {
        if (trash == null)
            return;

        bool correct = acceptedType == trash.type;

        // Busca TrashFeedback corretamente e evita null propagation
        var feedback = TrashFeedback.Instance;
        if (feedback != null)
        {
            feedback.ShowResult(correct);
        }

        // Atualiza pontuação/vidas no gerenciador de jogo
        var manager = TrashGameManager.Instance;
        if (manager != null)
        {
            manager.RegisterResult(correct);
        }

        // Destroi o item em ambos os casos
        Destroy(trash.gameObject);
    }   
}
