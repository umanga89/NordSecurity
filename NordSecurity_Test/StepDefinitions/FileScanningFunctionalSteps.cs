using NUnit.Framework;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using static NordSecurity_Test.Support.Enums;

namespace NordSecurity_Test.StepDefinitions
{

    [Binding]
    class FileScanningFunctionalSteps
    {
        ScanResult_t scanResult_t;

        [DllImport("C:\\Users\\laksh\\Downloads\\fakefilescan\\FakeFilescan.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int GetLibraryVersion();
        [DllImport("C:\\Users\\laksh\\Downloads\\fakefilescan\\FakeFilescan.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern ScanResult_t ScanFile(string? filePath, string sourceUrl);

        [Given(@"I have the file scanning library downloaded")]
        public void GivenIHaveTheFileScanningLibraryDownloaded()
        {
            //To be implemented later
        }

        [When(@"I invoke ScanFile method with filePath (.*) and sourceUrl (.*)")]
        public void WhenIInvokeScanFileMethodWithFilePathAndSourceUrl(string? filePath, string sourceUrl)
        {
            try
            {
                if (filePath.Equals("ChromeSetup.exe"))
                {
                    sourceUrl = "https://www.google.com/chrome/thank-you.html?statcb=1&installdataindex=empty&defaultbrowser=0#\"";
                } else if (filePath.Equals("null"))
                {
                    filePath = null;
                }
                scanResult_t = ScanFile(filePath, sourceUrl);
            }
            catch(Exception e)
            {
                Serilog.Log.Error("An ERROR occurred | {0} " + e.Message);
                throw e;
            }
        }

        [Then(@"I should get result as (.*)")]
        public void ThenIShouldGetResult(string scanResult)
        {
            try
            {
                Enum.TryParse(scanResult, out ScanResult_t expectedScanResult);
                Assert.AreEqual(expectedScanResult, scanResult_t, "Mismatch in expected and actual scan results");
            }
            catch (Exception e)
            {
                Serilog.Log.Error("An ERROR occurred | {0} " + e.Message);
                throw e;
            }
        }
    }
}