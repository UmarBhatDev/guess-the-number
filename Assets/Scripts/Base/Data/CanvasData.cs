using UnityEngine;

namespace GTN.Base.Data
{
    public struct CanvasData
    {
        public Canvas Canvas { get; }

        public CanvasData(Canvas canvas)
        {
            Canvas = canvas;
        }
    }
}