using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType acceptedType;

    public void ReceiveTrash(TrashItem trash)
    {
        Destroy(trash.gameObject);
    }
}
