param location string = resourceGroup().location
@minLength(36)
param msEntraUserObjectId string 
param existingMessageOracleApiName string = 'messageoracle'
param existingMessagingServiceBusName string  = 'messaging-servicebus'

module kvMessagingFlow './Modules/keyvault.bicep' = {
  name: 'kvMessagingFlow'
  params: {
    location: location
    keyVaultName: 'msgflwkv${uniqueString(resourceGroup().name, 'msgflw')}'
    objectId: msEntraUserObjectId
  }
}

var stMessageFlowName = 'msgflwstg${uniqueString(resourceGroup().name, 'msgflw')}'
module stMessageFlow './Modules/blobStorage.bicep' = {
  name: 'messagingBlobStorage'
  params: {
    location: location
    name: stMessageFlowName
    containerName: 'messages'
    roleId: 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b'
    msEntraUserObjectId: msEntraUserObjectId
  }
}

module appMessageFlow './Modules/webJob.bicep' = {
  name: 'messageflow'
  params: {
    location: location
    baseName: 'messageflow'
    appSettings: {
      ServiceBusConnection__fullyQualifiedNamespace: '${existingMessagingServiceBusName}.servicebus.windows.net'
      BlobConnection__serviceUri: stMessageFlow.outputs.storageEndpointBlob
      PersonalDataFetcher_ClientBaseUrl: 'https://cloudhackathon2023-apim.azure-api.net/messageoracle'
      PersonalDataFetcher_ClientSubscriptionKey: '@Microsoft.KeyVault(VaultName=msgflwkvrlzcnmr42xchy;Secret=OcpApimSubscriptionKey)'
    }
  }
}

module kvMessagingFlowAccess './Modules/keyVaultAccessPolicies.bicep' = {
  name: 'messageFlowApiKvAccess'
  dependsOn: [ kvMessagingFlow, appMessageFlow ]
  params: {
    keyVaultName: kvMessagingFlow.outputs.keyVaultName
    objectId: appMessageFlow.outputs.appServiceManagedIdentityId
  }
}

resource existingServiceBusNamespace 'Microsoft.ServiceBus/namespaces@2022-10-01-preview' existing = {
  name: existingMessagingServiceBusName
}

var sbRoleId = '4f6d3b9b-027b-4f4c-9142-0e5a2a2247e0' // Azure Service Bus Data Receiver
resource sbRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(existingServiceBusNamespace.id, sbRoleId, appMessageFlow.name)
  scope: existingServiceBusNamespace
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', sbRoleId)
    principalId: appMessageFlow.outputs.appServiceManagedIdentityId
    principalType: 'ServicePrincipal'
  }
}

resource existingStMessageFlow 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: stMessageFlowName
}

var stRoleId = 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b' // Storage Blob Data Owner
resource stRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(existingStMessageFlow.id, stRoleId, appMessageFlow.name)
  scope: existingStMessageFlow
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', stRoleId)
    principalId: appMessageFlow.outputs.appServiceManagedIdentityId
    principalType: 'ServicePrincipal'
  }
}
