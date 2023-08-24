using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ApplicationControllerSettings), menuName = AssetMenuName.SETTINGS + nameof(ApplicationControllerSettings))]
public class ApplicationControllerSettings : ScriptableObject
{
    public GameScreen GameScreenPrefab => gameScreenPrefab;
    public MainMenuScreenView MainMenuScreenPrefab => mainMenuScreenPrefab;

    [SerializeField] private GameScreen gameScreenPrefab;
    [SerializeField] private MainMenuScreenView mainMenuScreenPrefab;
}
