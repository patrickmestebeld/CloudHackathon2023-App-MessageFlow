param serviceBusNamespace string = 'sbns-brieven'
param managedIdentityId string = ''
param roleId string = ''

resource exserviceBusNamespace 'Microsoft.ServiceBus/namespaces@2017-04-01' existing = {
  name: serviceBusNamespace
}

resource sbRoleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(exserviceBusNamespace.id, roleId, managedIdentityId)
  scope: exserviceBusNamespace
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleId)
    principalId: managedIdentityId
    principalType: 'ServicePrincipal'
  }
}
