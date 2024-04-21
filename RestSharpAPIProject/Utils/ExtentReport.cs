﻿using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace RestSharpAPIProject.Utils
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenarios;

        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        public static string testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestResults");

        public static ExtentReports ExtentReportInit()
        {
            var htmlReporter = new ExtentHtmlReporter(testResultPath);
            htmlReporter.Config.ReportName = "Automation status Report";
            htmlReporter.Config.DocumentTitle = "Automations status report";
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Start();
            

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            return _extentReports;
        }

        public static void cleanReport()
        {
            _extentReports.Flush();
        }
    }
}