using System;
using System.Collections.Generic;
using System.Text;

namespace EA.AuditService.Client
{
    public class AuditServiceSettings
    {
        public string BaseAddress { get; set; }
        public CognitoSettings CognitoSettings { get; set; } = new CognitoSettings();

    }

}
