@minLength(3)
@maxLength(24)
param keyVaultName string
param location string = resourceGroup().location
param tenantId string = subscription().tenantId
param objectId string
param secrets secretType[] = []

type secretType = {
  name: string
  @secure()
  value: string
}

resource keyVault 'Microsoft.KeyVault/vaults@2021-11-01-preview' = {
  name: keyVaultName
  location: location
  properties: {
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: true
    tenantId: tenantId
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    accessPolicies: [
      {
        objectId: objectId
        tenantId: tenantId
        permissions: { keys: [ 'all' ], secrets: [ 'all' ] }
      }
    ]
    sku: {
      name: 'standard' // @allowed([ 'standard', 'premium' ])
      family: 'A'
    }
    networkAcls: {
      defaultAction: 'Allow'
      bypass: 'AzureServices'
    }
  }
}

resource KeyVaultSecrets 'Microsoft.KeyVault/vaults/secrets@2021-11-01-preview' = [for secret in secrets: {
  parent: keyVault
  name: secret.name
  properties: {
    value: secret.value
  }
}]

output keyVaultName string = keyVault.name
output keyVaultUri string = keyVault.properties.vaultUri
