using UnityEngine;
using DG.Tweening;

public class PastySpawn : MonoBehaviour
{
    private float cameraBoundsOffset = -3f;
    private Bounds mainCameraBounds;
    private void Start()
    {
        mainCameraBounds = GetCameraBounds(Camera.main);
        mainCameraBounds.Expand(cameraBoundsOffset);
        transform.DOMove(GetNewRandomPoint(), 1);
    }

    private Vector3 GetNewRandomPoint()
    {
        Vector3 newPoint = new Vector3(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1), 0);

        if (isOutOfBounds(newPoint))
        {
            newPoint = mainCameraBounds.ClosestPoint(newPoint);
            newPoint.Set(newPoint.x, newPoint.y, 0f);
        }
        return newPoint;
    }

    private bool isOutOfBounds(Vector3 targetPosition)
    {
        return mainCameraBounds.Intersects(new Bounds(targetPosition, GetComponent<BoxCollider2D>().bounds.size)) ? false : true;
    }

    private static Bounds GetCameraBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        return new Bounds(camera.transform.position, new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
    }
}