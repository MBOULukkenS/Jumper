using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEngine.UI.Extensions.Examples
{
    public class Example01Scene : MonoBehaviour
    {
        [SerializeField]
        Example01ScrollView scrollView = null;

        void Start()
        {
            List<Example01CellDto> cellData = Enumerable.Range(0, 20)
                .Select(i => new Example01CellDto { Message = "Cell " + i })
                .ToList();

            scrollView.UpdateData(cellData);
        }
    }
}
