using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Json;

namespace Diploma.Models
{
    public class CustomDashboardJsonDataSource : DashboardJsonDataSource
    {
        public void ChangeDataSource(string jsonValue = "", string root = "")
        {
            JsonSource = new CustomJsonSource(jsonValue);
            RootElement = root;
        }
    }
}