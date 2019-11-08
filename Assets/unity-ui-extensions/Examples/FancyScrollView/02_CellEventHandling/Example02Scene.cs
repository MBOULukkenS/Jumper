using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEngine.UI.Extensions.Examples
{
    public class Example02Scene : MonoBehaviour
    {
        [SerializeField]
        Example02ScrollView scrollView = null;

        void Start()
        {
            List<Example02CellDto> cellData = Enumerable.Range(0, 20)
                .Select(i => new Example02CellDto { Message = "Cell " + i })
                .ToList();

            scrollView.UpdateData(cellData);
        }
    }
}
