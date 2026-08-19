using UnityEngine;

public class PuzzleSpawnArea : MonoBehaviour
{
    private BoxCollider2D spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
    }

    public Vector3 GetRandomPosition()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(
            bounds.min.x,
            bounds.max.x
        );

        float y = Random.Range(
            bounds.min.y,
            bounds.max.y
        );

        return new Vector3(x, y, 0f);
    }

    public void DisableCollider()
    {
        spawnArea.enabled = false;
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D box =
            GetComponent<BoxCollider2D>();

        if (box == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(
            box.bounds.center,
            box.bounds.size
        );
    }
}
