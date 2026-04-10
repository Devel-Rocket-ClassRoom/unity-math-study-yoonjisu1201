using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool isReturning;
    public float timeReturn = 2f;
    public Vector3 originalPosition;
    private Vector3 startPosition;
    private float Timer;
    private Terrain terrain;

    private void Start()
    {
        terrain = Terrain.activeTerrain;
    }

    private void Update()
    {
        if (isReturning)
        {
            Timer = Time.deltaTime / timeReturn;
            Vector3 newPos = Vector3.Lerp(startPosition, originalPosition, Timer);
            newPos.y = terrain.SampleHeight(newPos);
            transform.position = newPos;
        }
    }
    public void Return()
    {
        isReturning = true;
        startPosition = transform.position;
    }

    public void DragEnd()
    {
        
    }

    public void DratStart()
    {
        isReturning = false;
        Timer = 0f;

        originalPosition = transform.position;
    }
    private void ResetDrag()
    {

    }
}
