using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackingService.Contract.DataTransfer;

public class TaskDto
{
    public long Id { get; set; }
    
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public long ProjectId { get; set; }
}