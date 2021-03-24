using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class WaySnake : MonoBehaviour
{
    // должен сохранять последние несколько точек пути для движения назад
    public List<Vector3> PositionOfWay;  // поменять на стек?
    public List<Quaternion> RotationOfWay;

    public  void SetPositionAndRotationOfWay(Vector3 pos, Quaternion rot)
    {
        if (PositionOfWay.Count == 100)
        {
            for (int i = 0; i < PositionOfWay.Count / 2; i++)
            {
                PositionOfWay.RemoveAt(i);
                RotationOfWay.RemoveAt(i);
            }
        }
        PositionOfWay.Add(pos);
        RotationOfWay.Add(rot);
    }

    public Vector3 GetPositionOfWay()
    {
        var pos = PositionOfWay[PositionOfWay.Count - 1];
        PositionOfWay.RemoveAt(PositionOfWay.Count - 1);
        return pos;
    }

    public Quaternion GetRotationOfWay()
    {
        var rot = RotationOfWay[RotationOfWay.Count - 1];
        RotationOfWay.RemoveAt(RotationOfWay.Count - 1);
        return rot;
    }
}
