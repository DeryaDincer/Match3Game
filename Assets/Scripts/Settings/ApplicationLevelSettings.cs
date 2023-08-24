using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(ApplicationLevelSettings), menuName = AssetMenuName.SETTINGS + nameof(ApplicationLevelSettings))]
public class ApplicationLevelSettings : ScriptableObject
{
    public LevelSettings[] LevelSettings => levelSettings;
    [SerializeField] private LevelSettings[] levelSettings;
}
