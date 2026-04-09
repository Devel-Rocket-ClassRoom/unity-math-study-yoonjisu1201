using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Camera camera;
    public LayerMask terrainLayer;
    public LayerMask targetLayer;


    private GameObject target;
    private Vector3 originalPos;
    private Vector3 targetYOffset = new Vector3(0f, 15f, 0f);
    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray targetRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit targetHit;

            if (!Physics.Raycast(targetRay, out targetHit, Mathf.Infinity, targetLayer))
            {
                Debug.Log("타겟 레이어가 아닙니다.");
                return;
            }
            else
            {
                Debug.Log("타겟 레이어가 맞습니다.");
                target = targetHit.collider.gameObject;
                originalPos = targetHit.point + targetYOffset;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (target == null) return;
            Debug.Log("누르고 있는 중. .");
            Ray terrainRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit terrainHit;

            if (Physics.Raycast(terrainRay, out terrainHit, Mathf.Infinity, terrainLayer))
            {
                target.transform.position = terrainHit.point + targetYOffset;
                target.transform.rotation = Quaternion.LookRotation(terrainHit.normal);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(ReturnToOrigin(target, originalPos));
        }
        Debug.Log("한 프레임 종료");
    }

    IEnumerator ReturnToOrigin(GameObject targetObj, Vector3 org)
    {
        float elapsed = 0f;
        float duration = 1f;
        Vector3 startPos = targetObj.transform.position;

        while (elapsed < duration)
        {
            if (targetObj == null) yield break;
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            Vector3 nextPos = Vector3.Lerp(startPos, org, t);
            float terrainHeight = Terrain.activeTerrain.SampleHeight(nextPos);  //터레인 Y높이
            float finalY = terrainHeight + Terrain.activeTerrain.transform.position.y;  //진짜 Y높이

            targetObj.transform.position = new Vector3(nextPos.x, finalY, nextPos.z) + targetYOffset;  //y축 터레인 높이 따라 제자리로 돌아가기

            yield return null;
        }

        targetObj.transform.position = org;
        targetObj = null;
        originalPos = Vector3.zero;
    }
}
