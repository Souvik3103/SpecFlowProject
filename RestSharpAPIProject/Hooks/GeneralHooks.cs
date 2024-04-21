using System;
using RestSharpAPIProject.Drivers;
using TechTalk.SpecFlow;
using RestSharpAPIProject.Utils;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace RestSharpAPIProject.Hooks
{
    public sealed class GeneralHooks
    {
        [Binding]
        public sealed class Hooks : ExtentReport
        {

            [BeforeScenario]
            public static void BeforeTestRun(ScenarioContext scenarioContext)
            {
                ExtentReportInit();
                _extentReports.CreateTest(scenarioContext.ScenarioInfo.Title, scenarioContext.ScenarioInfo.Description);
                scenarioContext.Add("Reporter", _extentReports);
            }

            [AfterScenario]
            public static void AfterTestRun()
            {
                cleanReport();
            }
        }
    }
}
