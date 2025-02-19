# Intelligent Image Processing with OpenAI on Azure

This project demonstrates how to process and classify images using OpenAI's models deployed on Azure AI resources. It leverages Azure Blob Storage for storing images, Azure Event Grid to trigger processing, Azure Functions for backend logic, and Azure Cosmos DB for storing image metadata.

## Architecture Overview
The solution utilizes OpenAI's deployed model on Azure, which integrates with Azure AI services for image classification and metadata extraction. When an image is uploaded, the system processes it using the OpenAI model to derive insights and store results in Cosmos DB.

![Architecture Diagram](https://private-user-images.githubusercontent.com/113801007/414799870-5346ce36-2b49-457e-bab4-128162813dfd.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3Mzk5ODEwODMsIm5iZiI6MTczOTk4MDc4MywicGF0aCI6Ii8xMTM4MDEwMDcvNDE0Nzk5ODcwLTUzNDZjZTM2LTJiNDktNDU3ZS1iYWI0LTEyODE2MjgxM2RmZC5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjUwMjE5JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI1MDIxOVQxNTU5NDNaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT1jNTRjMDNiNDI5Y2E1Yzg3ZDI4MmRjODQ5ODdiNjUxNTY2MDNjZmUzY2U5MGEzMjgyOTJiODgwMzJhZDhjZjE3JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.sFfNF-PnoBVSNqRNL560e2HFHa29UgdcHHjSG2wnp64)

## Key Components

- **OpenAI Deployed Model on Azure AI**: The GPT-4o model deployed on Azure AI processes images and extracts metadata or classifications.
- **Azure Functions**: Manages the image processing pipeline by calling the OpenAI model and handling the image data.
- **Azure Event Grid**: Triggers the processing pipeline whenever an image is uploaded to Azure Blob Storage.
- **Azure Blob Storage**: Stores the images that need to be processed.
- **Azure Cosmos DB**: Stores metadata extracted from the images, including classification results and other insights.

## How It Works
1. **Upload Image**: An image is uploaded to Azure Blob Storage.  You can easily upload an image using the web app here: [Upload Image Web App](https://smartimageuploader.xyz/).
2. **Trigger Event**: The upload event triggers Azure Event Grid, which notifies an Azure Function.
3. **Process Image**: The Azure Function sends the image to the OpenAI deployed model via Azure AI services for classification and analysis.
4. **Store Metadata**: After processing, the image metadata, including classification results, is stored in Azure Cosmos DB.
5. **Consume Data**: The processed metadata accessed by front-end application.



## Project Details
This project aims to demonstrate an efficient and scalable architecture for intelligent image processing using Azure's cloud platform combined with OpenAI's models. It supports a variety of use cases including:
- **E-commerce**: Automatically classifying and tagging product images.
- **Insurance**: Automating claims processing through image recognition.
- **Healthcare**: Analyzing medical images for faster diagnosis.


## Setup Instructions
To run this project, follow these steps:
1. Set up your **Azure Blob Storage** to store images.
2. Deploy the **OpenAI model** on Azure AI.
3. Configure **Azure Functions** to handle the image processing pipeline.
4. Set up **Azure Cosmos DB** to store the processed metadata.

also

1. **Install .NET 9 SDK**  
   - Download and install the [**.NET 9 SDK**](https://dotnet.microsoft.com/download).

2. **Install Azure Functions Core Tools**  
   - Install the [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local) to develop and test functions locally.

3. **Set Up Local Settings**  
   - In the root of the **backend** folder, create a `local.settings.json` file with the following structure:

   ```json
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsSecretStorageType": "files",
       "AzureWebJobsStorage": "<Your Azure Storage connection string>",
       "ContainerName": "upload",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
       "AZURE_OPENAI_ENDPOINT": "<Your Azure OpenAI endpoint>",
       "AZURE_OPENAI_KEY": "<Your Azure OpenAI key>",
       "CosmosDbConnectionSetting": "<Your Cosmos DB connection string>"
     }
   }

## Additional Information
For a detailed overview of the architecture, you can visit the official [Azure Architecture](https://learn.microsoft.com/en-us/azure/architecture/ai-ml/idea/intelligent-apps-image-processing) page.

### Tools and Technologies
- **Azure Blob Storage**: To store images.
- **Azure Event Grid**: To handle events.
- **Azure Functions**: Serverless backend logic.
- **Azure Cosmos DB**: NoSQL database for storing metadata.
- **OpenAI Model**: Deployed on Azure for image classification.



