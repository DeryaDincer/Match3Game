using Cinemachine;
using System;
using UnityEngine;

[Serializable]
public class GameInCameraControllerReferences
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera VirtualCamera => virtualCamera;
}
