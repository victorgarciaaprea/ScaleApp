using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scale.Models
{
    public class WorkOrder
    {
        public Recipe Recipe { get; set; }
        public String Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public WorkOrderStatus Status { get; set; } 
        public List<WorkOrderPart> Parts { get; set; }
    }

    public class WorkOrderPart
    {
        public WorkOrderPart()
        {
            Items = new List<RecipeItem>();
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PerformedBy { get; set; }
        public string ExceptionBy { get; set; }
        public string ExceptionReason { get; set; }
        public List<RecipeItem> Items { get; set; }
        public string ScaleId { get; set; }
        public WorkOrderStatus Status { get; set; }
    }

    public enum WorkOrderStatus
    {
        NotStarted, InProgress, Completed, Failed, Aborted, Deleted
    }
    public enum WorkOrderPartStatus
    {
        NotStarted, InProgress, Completed, Failed
    }
}
