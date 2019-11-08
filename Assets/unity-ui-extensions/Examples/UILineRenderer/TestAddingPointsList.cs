using System.Collections.Generic;

namespace UnityEngine.UI.Extensions.Examples
{
    public class TestAddingPointsList : MonoBehaviour
    {
        public UILineRendererList LineRenderer;
        public Text XValue;
        public Text YValue;

        // Use this for initialization
        public void AddNewPoint()
        {
            Vector2 point = new Vector2() { x = float.Parse(XValue.text), y = float.Parse(YValue.text) };
            LineRenderer.AddPoint(point);
        }

        public void ClearPoints()
        {
            if (LineRenderer != null && LineRenderer.Points != null)
            {
                LineRenderer.ClearPoints();
            }
        }
    }
}