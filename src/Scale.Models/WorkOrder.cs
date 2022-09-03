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
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public WorkOrderStatus Status { get; set; } 
    }

    public enum WorkOrderStatus
    {
        Created, InProgress, Completed, Aborted, Failed, Deleted
    }
}
