using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private float parallaxFactor;
    private GameObject backgroundPrefab;
    private GameObject[] backgroundInstanceArray = new GameObject[4];

    //the width hight of a backGround prefab
    float gridWidthHeight = 64;

    public void Initialize(float parallaxFactor, GameObject backgroundPrefab)
    {
        this.parallaxFactor = parallaxFactor;
        this.backgroundPrefab = backgroundPrefab;
        CreateBackgroundInstanceArray();
    }

    public void UpdatePosition(Vector3 startPostion, Vector3 subjectMoveDistance)
    {
        Vector2 position = startPostion + subjectMoveDistance * parallaxFactor;
        transform.position = position;
    }

    public void UpdateBackgroundArrayPosition(Vector3 subjectMoveDistance)
    {
        Vector2 subjectGridPosition = GetSubjectGridPosition(subjectMoveDistance);

        backgroundInstanceArray[0].transform.localPosition = subjectGridPosition * gridWidthHeight;
        backgroundInstanceArray[1].transform.localPosition = subjectGridPosition * gridWidthHeight + new Vector2(0, -gridWidthHeight);
        backgroundInstanceArray[2].transform.localPosition = subjectGridPosition * gridWidthHeight + new Vector2(-gridWidthHeight, -gridWidthHeight);
        backgroundInstanceArray[3].transform.localPosition = subjectGridPosition * gridWidthHeight + new Vector2(-gridWidthHeight, 0);
    }

    private void CreateBackgroundInstanceArray()
    {
        for (int i = 0; i < 4; i++)
        {
            backgroundInstanceArray[i] = GameObject.Instantiate(backgroundPrefab, transform);
        }
    }

    private Vector2 GetSubjectGridPosition(Vector3 subjectMoveDistance)
    {
        //the target relative to the parallax factor (how much the subject moved in the space of this layer)
        Vector3 relativeSubjectMoveDistance = subjectMoveDistance * (1 - parallaxFactor);

        int gridPositionX = 0;
        int gridPositionY = 0;

        if (subjectMoveDistance.x > 0)
            gridPositionX = Mathf.CeilToInt((relativeSubjectMoveDistance.x - gridWidthHeight / 2) / gridWidthHeight);

        if (subjectMoveDistance.x < 0)
            gridPositionX = Mathf.FloorToInt((relativeSubjectMoveDistance.x + gridWidthHeight / 2) / gridWidthHeight);

        if (subjectMoveDistance.y >= 0)
            gridPositionY = Mathf.CeilToInt((relativeSubjectMoveDistance.y - gridWidthHeight / 2) / gridWidthHeight);

        if (subjectMoveDistance.y < 0)
            gridPositionY = Mathf.FloorToInt((relativeSubjectMoveDistance.y + gridWidthHeight / 2) / gridWidthHeight);

        return new Vector2(gridPositionX, gridPositionY);
    }
}
