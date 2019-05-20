using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class ModelScaler : MonoBehaviour
{
    [SerializeField] Transform modelTop;
    [SerializeField] Transform modelBottom;
    [SerializeField] Transform modelLeft;
    [SerializeField] Transform modelRight;

    [SerializeField] Transform modelRoot;

    [SerializeField] float modelFillRate = 0.8f;

    void OnRectTransformDimensionsChange()
    {
        UpdateLayout();
    }

    

    void UpdateLayout()
    {
        RectTransform trans = (RectTransform)transform;

        Vector3[] corners = new Vector3[4];
        trans.GetWorldCorners(corners);

        Vector3 center = (corners[0] + corners[1] + corners[2] + corners[3]) / 4;

        Vector3 modelCenter = new Vector3(
            (modelLeft.position.x + modelRight.position.x) / 2,
            (modelTop.position.y + modelBottom.position.y) / 2,
            0
        );

        Vector3 modelCenterDiff = modelCenter - modelRoot.position;

        modelRoot.position = center + modelCenterDiff;

        float width = corners[2].x - corners[0].x;
        float xRate = (modelRight.position.x - modelLeft.position.x) / width;

        float height = corners[2].y - corners[0].y;
        float yRate = (modelTop.position.y - modelBottom.position.y) / height;

        if (xRate > 0 && yRate > 0 && width > 0 && height > 0)
        {
            float scale = Mathf.Min(
                modelRoot.localScale.x * modelFillRate / xRate,
                modelRoot.localScale.y * modelFillRate / yRate
            );

            modelRoot.localScale = new Vector3(scale, scale, 200);
        }
    }
}
