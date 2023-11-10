param location string = resourceGroup().location
@allowed([
  '2a2b9908-6ea1-4ae2-8e65-a410df84e7d1' // Storage Blob Data Reader
  'ba92f5b4-2d11-453d-a403-e96b0029c9fe' // Storage Blob Data Contributor
  'b7e6dc6d-f1e8-4753-8033-0f276bb0955b' // Storage Blob Data Owner
])
param roleId string
param msEntraUserObjectId string

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: 'stg${uniqueString(resourceGroup().name)}'
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_RAGRS'
  }
  properties: {
    allowSharedKeyAccess: false
    allowBlobPublicAccess: true
    accessTier: 'Hot'
    supportsHttpsTrafficOnly: true
  }
}

resource container 'Microsoft.Storage/storageAccounts/blobServices/containers@2022-09-01' = {
#disable-next-line use-parent-property
  name: '${storageAccount.name}/default/messages'
  properties: {
    publicAccess: 'Blob'
  }
}

// Assign role to give acces to the service bus namespace for user
resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(storageAccount.id, roleId, msEntraUserObjectId)
  scope: storageAccount
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleId)
    principalId: msEntraUserObjectId
    principalType: 'User'
  }
}

// Return managed identity id
output storageAccountName string = storageAccount.name
output storageAccountResourceId string = storageAccount.id
output storageEndpointBlob string = storageAccount.properties.primaryEndpoints.blob
