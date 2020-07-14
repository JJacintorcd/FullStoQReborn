using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.RD.FullStoQReborn.BusinessLayer.OperationResults
{
    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }
}
