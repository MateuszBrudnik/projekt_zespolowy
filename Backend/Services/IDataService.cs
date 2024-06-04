using System;
namespace Projekt.Services
{
    public interface IDataService
    {
        void ImportCsv(StreamReader csvStream);
        string ExportCsv();
    }
}

