using System;
using System.Collections.Generic;
using System.Text;

namespace Devices.Application.Interfaces
{
    public interface IDataSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken);
    }
}
