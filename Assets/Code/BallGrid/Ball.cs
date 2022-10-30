using System.Collections;
using System.Collections.Generic;
using Code.BallGridLogic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Ball[] neighbors = new Ball[6];
    public BallGrid Grid { get; set; }
}
