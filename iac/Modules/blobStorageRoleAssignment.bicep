param stMessageFlowName string = 'sbns-brieven'
param managedIdentityId string = ''
param roleId string  = 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b' // Storage Blob Data Owner

resource existingStMessageFlow 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: stMessageFlowName
}

resource stRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(existingStMessageFlow.id, roleId, managedIdentityId)
  scope: existingStMessageFlow
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleId)
    principalId: managedIdentityId
    principalType: 'ServicePrincipal'
  }
}
