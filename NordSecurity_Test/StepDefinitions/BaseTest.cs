using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Steps
{
    public class BaseTest
    {
        public Dictionary<String, String> scenarioTestData;

        public void SetScenarioData(Table table)
        {
            scenarioTestData = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                scenarioTestData.Add(row[0], row[1]);
            }
        }

        public Dictionary<String,String> GetScenarioData()
        {
            return scenarioTestData;
        }
        public void ClearScenarioData()
        {
            scenarioTestData.Clear();
        }

    }
}
