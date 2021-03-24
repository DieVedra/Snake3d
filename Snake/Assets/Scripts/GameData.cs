using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public int[] ScoreToCompleteTheDay;
    public int[] DurationOfDays;

    [Header("Needed to load the last level scene")]
    public int LastSceneIndex;
    [Space]
    public int MaxOpenLvlSceneIndex;
    public int MaxOpenDay;

    [Header("Сarries an index for the lighting array")]
    public int LastCurrentDay; // первый день с индексом 0

    [Space]
    [Header("Full score for all levels")]
    public int FullScore;


    public float DifficultyLevelOfTheDay;

}
