using System.Configuration;

namespace twbobibobi.Data
{
    public class BaseDAC
    {
        protected string ConnString = "";
        public BaseDAC()
        {
            ConnString = ConfigurationManager.ConnectionStrings["DBHost"].ConnectionString;
        }
    }
}