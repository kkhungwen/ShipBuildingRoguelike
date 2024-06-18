using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private ParallaxLayerManager parallaxBackground;
    [SerializeField] private Transform cameraTransform;
    private void Start()
    {
        //test
        transform.position = new Vector3(16 * Settings.chunkWidth, 16 * Settings.chunkHeight);

        parallaxBackground.InitializeParallaxBackGround(cameraTransform);
    }
}


