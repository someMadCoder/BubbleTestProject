using UnityEngine;

namespace Code.BallGridLogic
{
    /// <summary>
    /// Generator working by wave function collapsed method
    /// </summary>
    public class BallGridWFCGenerator
    {
        [SerializeField] private Ball _ballPrefab;

        public void Generate(int lines, params BallColor[] color)
        {
            for(int x = 0; x<10; x++)
            for (int y = 0; y < lines; y++)
            {
                
            }
        }
        
    }
}