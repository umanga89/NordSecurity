using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SpecFlowFramework.Steps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Hooks
{
    [Binding]
    public sealed class Hooks : BaseTest
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public BaseTest baseTest;
        public Hooks(BaseTest baseTestObj)
        {
            this.baseTest = baseTestObj;
        }

        public static AventStack.ExtentReports.ExtentReports extent;
        [ThreadStatic]
        public static ExtentTest feature;
        public ExtentTest scenario;
        public ExtentTest step;
        public static string reportPath = System.IO.Directory.GetParent(@"../../../").FullName + Path.DirectorySeparatorChar + "Result" +Path.DirectorySeparatorChar;
        public static string configFilePath = System.IO.Directory.GetParent(@"../../../").FullName + Path.DirectorySeparatorChar + "Config/config.json";
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(reportPath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReport);

            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);
            Log.Logger = new LoggerConfiguration().MinimumLevel.ControlledBy(levelSwitch).WriteTo.File(reportPath + @"\Logs.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {Level:u3} | {Message} {NewLine}", rollingInterval: RollingInterval.Day).CreateLogger();

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = extent.CreateTest(context.FeatureInfo.Title);
            Log.Information("Feature file {0} has started executing", context.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {

            //create new node in extent report
            scenario = feature.CreateNode(context.ScenarioInfo.Title);

            Log.Information("Scenario {0} has started executing", context.ScenarioInfo.Title);
        }

        [BeforeStep]
        public void BeforeStep()
        {
            step = scenario;
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            if (context.TestError == null)
            {
                Log.Information("Step {0} has executed successfully!", context.StepContext.StepInfo.Text);
                step.Log(Status.Pass, context.StepContext.StepInfo.Text);
            }
            else if(context.TestError != null)
            {
                string error = context.TestError.StackTrace;
                Log.Error("Step has FAILED | {0} ", context.StepContext.StepInfo.Text);
                step.Log(Status.Fail, context.StepContext.StepInfo.Text);
                step.Log(Status.Fail, context.TestError.Message);
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext context)
        {
            Log.Information("Scenario {0} has ended executing", context.ScenarioInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext context)
        {
            Log.Information("Feature file {0} has ended executing", context.FeatureInfo.Title);
            extent.Flush();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("This is executed AFTER ending test run");
        }
    }
}
