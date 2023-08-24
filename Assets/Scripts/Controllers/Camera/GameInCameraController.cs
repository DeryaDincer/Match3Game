using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInCameraController : IInitializable
{
    private CinemachineVirtualCamera vCam;
    private float cameraSizeOffset = 1f;
    private int width;
    private int height;

    #region Injection
    private GameInCameraControllerReferences references;
    private LevelSceneReferences levelSceneReferences;
    private LevelController levelController;

    [Inject]
    public void Consturct(LevelSceneReferences references,LevelController levelController)
    {
        this.levelSceneReferences = references; 
        this.levelController = levelController;
    }
    #endregion

    public void Initialize()
    {
        references = levelSceneReferences.GameInCameraControllerReferences;

        // Get the width and height of the game board from the current level's settings.
        width = (int)levelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings.Width;
        height = (int)levelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings.Height;

        // Initialize the virtual camera and set its parameters.
        this.vCam = references.VirtualCamera;
        SetCamera();
    }

    // Set the orthographic size of the camera based on the game board's dimensions.
    private void SetCamera()
    {
        int orthographicSize = Mathf.Max(width, height);
        vCam.m_Lens.OrthographicSize = orthographicSize + cameraSizeOffset;
    }
}
