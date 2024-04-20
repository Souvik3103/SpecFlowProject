

namespace SpecFlowProject1.Helpers
{
    public class SystemUnderTest
    {
        public string AppUrl { get; set; }
        
        public SystemUnderTest()
        {
            AppUrl = "https://www.amazon.in";
        }

        public string GetAppUrl() { return AppUrl; }
    }
}
