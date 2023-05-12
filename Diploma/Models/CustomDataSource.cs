using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Json;

namespace Diploma.Models
{
    public class CustomDataSource : DashboardJsonDataSource
    {
        public CustomDataSource(string sourceName, string jsonData) : base(sourceName)
        {
            ChangeDataSource(jsonData);
        }

        public void ChangeDataSource(string jsonData)
        {
            JsonSource = new CustomJsonSource(jsonData);
            RootElement = "Value";
        }
    }
}