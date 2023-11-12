@maxLength(8)
param baseName string = 'msgT01'
@allowed(['dev', 'prd'])
param environment string = 'dev'
param location string = resourceGroup().location
@minLength(36)
param msEntraUserObjectId string 
param existingMessagingServiceBusName string = format('sbns-{0}-{1}-{2:D3}', baseName, environment, 1)

module kvMessagingFlow './Modules/keyvault.bicep' = {
  name: 'kvMessagingFlow'
  params: {
    location: location
    environment: environment
    objectId: msEntraUserObjectId
  }
}

module stMessagingFlow './Modules/blobStorage.bicep' = {
  name: 'stMessageFlow'
  params: {
    location: location
    roleId: 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b' // Storage Blob Data Owner
    msEntraUserObjectId: msEntraUserObjectId
  }
}

resource existingApim 'Microsoft.ApiManagement/service@2021-08-01' existing = {
  name: format('apim-{0}-{1}-{2:D3}', baseName, environment, 1)
}

module appMessagingFlow './Modules/webJob.bicep' = {
  name: 'messageflow'
  dependsOn: [ kvMessagingFlow, stMessagingFlow ]
  params: {
    location: location
    baseName: '${baseName}flow'
    environment: environment
    appSettings: {
      ServiceBusConnection__fullyQualifiedNamespace: '${existingMessagingServiceBusName}.servicebus.windows.net'
      BlobConnection__serviceUri: stMessagingFlow.outputs.storageEndpointBlob
      PersonalDataFetcher_ClientBaseUrl: '${existingApim.properties.gatewayUrl}/oracle'
      PersonalDataFetcher_ClientSubscriptionKey: '@Microsoft.KeyVault(VaultName=${kvMessagingFlow.outputs.keyVaultName};Secret=OcpApimSubscriptionKey)'
      MessageInbox_ClientBaseUrl: '${existingApim.properties.gatewayUrl}/inbox'
      FunctionsOptions_BlobConnectionString: stMessagingFlow.outputs.storageEndpointBlob
    }
  }
}

module kvMessagingFlowAccess './Modules/keyVaultAccessPolicies.bicep' = {
  name: 'kvMessagingFlowAccess'
  dependsOn: [ appMessagingFlow ]
  params: {
    keyVaultName: kvMessagingFlow.outputs.keyVaultName
    objectId: appMessagingFlow.outputs.appServiceManagedIdentityId
  }
}

module sbMessagingRoleAssignment './Modules/serviceBusRoleAssignment.bicep' = {
  name: 'sbMessagingRoleAssignment'
  dependsOn: [ appMessagingFlow ]
  params: {
    serviceBusNamespace: existingMessagingServiceBusName
    managedIdentityId: appMessagingFlow.outputs.appServiceManagedIdentityId
    roleId: '4f6d3b9b-027b-4f4c-9142-0e5a2a2247e0' // Azure Service Bus Data Receiver
  }
}

module stgRoleAssignment './Modules/blobStorageRoleAssignment.bicep' = {
  name: 'stgRoleAssignment'
  dependsOn: [ stMessagingFlow, appMessagingFlow ]
  params: {
    stMessageFlowName: stMessagingFlow.outputs.storageAccountName
    managedIdentityId: appMessagingFlow.outputs.appServiceManagedIdentityId
  }
}
