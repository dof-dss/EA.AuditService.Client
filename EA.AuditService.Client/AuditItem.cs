using System;
using System.Collections.Generic;
using System.Text;

namespace EA.AuditService.Client
{
    public class AuditItem
    {
        public long SubjectId { get; set; }
        public string Subject { get; set; }
        public long ActorId { get; set; }
        public string Actor { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
    }
}
