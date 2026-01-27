using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
