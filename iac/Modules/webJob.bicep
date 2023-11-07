param baseName string = resourceGroup().name
param env string = 'dev'
param appSettings object = {}
param location string = resourceGroup().location
var baseNameLower = toLower(baseName)

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'asp-${baseNameLower}-${env}-001'
  location: location
  sku: {
    name: 'S1' // @allowed([ 'F1', 'D1', 'B1', 'B2', 'B3', 'S1', 'S2', 'S3', 'P1', 'P2', 'P3', 'P4' ])
  }
}
 
resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: 'app-${baseNameLower}-${env}-001'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
  }
}
 
resource config 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: appService
  name: 'web'
  properties: {
    alwaysOn: true
  }
}
 
// Add app settings
resource siteconfig 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: appService
  name: 'appsettings'
  properties: union(appSettings, {
   APPINSIGHTS_INSTRUMENTATIONKEY: appInsights.properties.InstrumentationKey
  })
}
 
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'app-${baseNameLower}-${env}-001'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
} 

output appServiceManagedIdentityId string = appService.identity.principalId


// resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-06-01-preview' existing = {
//   name: 'my-service-bus-namespace'

//   location: location
//   sku: {
//     name: 'Standard'
//   }
// }


// resource appRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
//   name: guid(baseName, 'AppServiceToServiceBus')
//   scope: serviceBusNamespace.id
//   properties: {
//     principalId: appService.identity.principalId
//     roleDefinitionId: 'f5a34d97-0cd6-4f93-9a8c-7d376a3e8c6d' // Contributor role
//   }
// }
