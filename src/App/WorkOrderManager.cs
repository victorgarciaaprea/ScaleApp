using Scale.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Scale
{
    internal class WorkOrderManager
    {
        internal WorkOrder Find(string workOrderId)
        {
            var content = File.ReadAllText("wo-001.json");
            var wo = JsonSerializer.Deserialize(content, typeof(WorkOrder)) as WorkOrder;
            if (wo.Id == workOrderId)
            {
                return wo;
            }

            return null;
        }


        internal WorkOrderPart GetNextPart(WorkOrder wo)
        {
            switch (wo.Status)
            {
                case WorkOrderStatus.NotStarted:
                    return wo.Parts[0];

                case WorkOrderStatus.Aborted:
                case WorkOrderStatus.Deleted:
                case WorkOrderStatus.Completed:
                case WorkOrderStatus.Failed:
                    return null;
            }

            foreach (WorkOrderPart part in wo.Parts)
            {
                if (part.Status != WorkOrderStatus.NotStarted)
                    continue;

                return part;
            }

            return null;
        }
    }
}
