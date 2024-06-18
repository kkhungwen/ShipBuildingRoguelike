using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayerManager : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayerData
    {
        public string name;
        public GameObject backgroundPrefab;
        public float parallaxFactor;
    }

    [SerializeField] private ParallaxLayerData[] parallaxLayerDataArray;

    private float checkSubjectGridPositionTime = 0.5f;
    private float checkSubjectGridPositionTimeCount = 0f;

    private List<ParallaxLayer> parallaxLayerList = new List<ParallaxLayer>();

    private Transform subjectTransform;
    private Vector3 startPosition;

    private void Update()
    {
        if (subjectTransform != null)
        {
            Vector3 subjectMoveDistance = subjectTransform.position - startPosition;

            UpdateParallaxLayerPosition(subjectMoveDistance);

            checkSubjectGridPositionTimeCount += Time.deltaTime;
            if (checkSubjectGridPositionTimeCount >= checkSubjectGridPositionTime)
            {
                UpdateParallaxLayerBackGroundObjectPosition(subjectMoveDistance);
                checkSubjectGridPositionTimeCount = 0;
            }
        }
    }

    private void UpdateParallaxLayerPosition(Vector3 subjectMoveDistance)
    {
        foreach (ParallaxLayer parallaxLayer in parallaxLayerList)
        {
            parallaxLayer.UpdatePosition(startPosition, subjectMoveDistance);
        }
    }

    //Update the instantiate grid object position to match subject position
    private void UpdateParallaxLayerBackGroundObjectPosition(Vector3 subjectMoveDistance)
    {
        foreach (ParallaxLayer parallaxLayer in parallaxLayerList)
        {
            parallaxLayer.UpdateBackgroundArrayPosition(subjectMoveDistance);
        }
    }

    public void InitializeParallaxBackGround(Transform subjectTransform)
    {
        this.subjectTransform = subjectTransform;
        startPosition = subjectTransform.position;
        CreateParallaxLayer();
    }

    private void CreateParallaxLayer()
    {
        foreach (ParallaxLayerData parallaxLayerData in parallaxLayerDataArray)
        {
            GameObject parallaxLayerObject = new GameObject("Layer_" + parallaxLayerData.name);
            parallaxLayerObject.transform.parent = transform;
            ParallaxLayer parallaxLayer = parallaxLayerObject.AddComponent<ParallaxLayer>();
            parallaxLayer.Initialize(parallaxLayerData.parallaxFactor, parallaxLayerData.backgroundPrefab);

            parallaxLayerList.Add(parallaxLayer);
        }
    }
}
