. .\PsFunctions\ReadEnvVariables.ps1
$resourceGroupName = "rg-$env:BASE_NAME-$env:ENVIRONMENT"

# Create resource group
az group create --name $resourceGroupName --location "northeurope"

# Deploy Bicep file
az deployment group create `
    --resource-group $resourceGroupName `
    --template-file "../main.bicep" `
    --parameters `
        baseName="$env:BASE_NAME" `
        environment="$env:ENVIRONMENT" `
        msEntraUserObjectId="$env:MS_ENTRA_USER_OBJECT_ID"