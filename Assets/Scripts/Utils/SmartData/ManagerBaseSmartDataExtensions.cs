using System.Collections.Generic;
using System.Linq;
using Jumper.Managers.Base;
using SmartData.SmartEvent;

namespace Utils.SmartData
{
    public static class ManagerBaseSmartDataExtensions
    {
        public static void InitializeListeners(this object obj)
        {
            IEnumerable<EventListener> listeners = obj.GetType()
                                                          .GetFields()
                                                            .Where(f => f.FieldType == typeof(EventListener))
                                                            .Select(f => f.GetValue(obj) as EventListener)
                                                            .Where(l => l != null);
                     
            foreach (EventListener listener in listeners)
                listener.unityEventOnReceive = true;
        }
    }
}