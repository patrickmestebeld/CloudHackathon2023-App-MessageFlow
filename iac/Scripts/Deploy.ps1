. .\PsFunctions\ReadEnvVariables.ps1

# Create resource group
az group create --name $env:RESOURCE_GROUP_NAME --location "northeurope"

# Deploy Bicep file
az deployment group create `
    --name "cloudhackathon-deployment" `
    --resource-group $env:RESOURCE_GROUP_NAME `
    --template-file "../main.bicep" `
    --parameters msEntraUserObjectId="$env:MS_ENTRA_USER_OBJECT_ID"