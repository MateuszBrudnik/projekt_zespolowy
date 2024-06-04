using System;
namespace Projekt.Services
{
    public interface IWarningService
    {
        IEnumerable<string> GenerateWarnings();
    }
}

