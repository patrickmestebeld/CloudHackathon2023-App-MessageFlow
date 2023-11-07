

var sbRoleId = '4f6d3b9b-027b-4f4c-9142-0e5a2a2247e0' // Azure Service Bus Data Receiver
resource sbRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(serviceBusNamespace.id, sbRoleId, msEntraUserObjectId)
  scope: serviceBusNamespace
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', sbRoleId)
    principalId: appMessageFlow.outputs.appServiceManagedIdentityId
    principalType: 'ServicePrincipal'
  }
}
