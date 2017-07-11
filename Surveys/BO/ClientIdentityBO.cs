using System;
using System.Collections.Generic;
using System.Text;

namespace Surveys.BO
{
    public class ClientIdentityBO
    {
        public Guid Id { get; set; }
        public bool IsAnonymous => Id == Guid.Empty;
    }
}
