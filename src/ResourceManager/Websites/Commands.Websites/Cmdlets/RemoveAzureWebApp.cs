﻿
// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------


using System.Management.Automation;
using Microsoft.Azure.Commands.WebApp.Properties;
using Microsoft.Azure.Commands.WebApp.Utilities;

namespace Microsoft.Azure.Commands.WebApp.Cmdlets
{
    /// <summary>
    /// this commandlet will let you delete an Azure web app
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRMWebApp")]
    public class RemoveAzureWebAppCmdlet : WebAppBaseNotMandatoryCmdlet
    {

       //always delete the slots, 
        private bool deleteSlotsByDefault = true;

        // leave behind the empty webhosting plan 
        private bool deleteEmptyServerFarmByDefault = false;

        //always delete the metrics
        private bool deleteMetricsByDefault = true;

        [Parameter(Mandatory = false, HelpMessage = "Do not ask for confirmation.")]
        public SwitchParameter Force { get; set; }
            
        protected override void ProcessRecord()
        {
            // Currently we delete all slots.
            string slotName = null;

            ConfirmAction(
                Force.IsPresent,
                string.Format(Resources.RemoveWebsiteWarning, Name),
                Resources.RemoveWebsiteMessage,
                Name,
                () => WebsitesClient.RemoveWebsite(ResourceGroupName, Name, slotName, deleteEmptyServerFarmByDefault, deleteMetricsByDefault, deleteSlotsByDefault));
        }

    }
}



