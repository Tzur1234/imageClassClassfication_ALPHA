# Intelligent Image Processing with OpenAI on Azure

This project demonstrates how to process and classify images using OpenAI's models deployed on Azure AI resources. It leverages Azure Blob Storage for storing images, Azure Event Grid to trigger processing, Azure Functions for backend logic, and Azure Cosmos DB for storing image metadata.

## Architecture Overview
The solution utilizes OpenAI's deployed model on Azure, which integrates with Azure AI services for image classification and metadata extraction. When an image is uploaded, the system processes it using the OpenAI model to derive insights and store results in Cosmos DB.

![Architecture Diagram](https://learn.microsoft.com/en-us/azure/architecture/ai-ml/idea/_images/architecture-intelligent-apps-image-processing.png)

## Key Components

- **OpenAI Deployed Model on Azure AI**: The GPT-4o model deployed on Azure AI processes images and extracts metadata or classifications.
- **Azure Functions**: Manages the image processing pipeline by calling the OpenAI model and handling the image data.
- **Azure Event Grid**: Triggers the processing pipeline whenever an image is uploaded to Azure Blob Storage.
- **Azure Blob Storage**: Stores the images that need to be processed.
- **Azure Cosmos DB**: Stores metadata extracted from the images, including classification results and other insights.

## How It Works
1. **Upload Image**: An image is uploaded to Azure Blob Storage.  You can easily upload an image using the web app here: [Upload Image Web App](#).
2. **Trigger Event**: The upload event triggers Azure Event Grid, which notifies an Azure Function.
3. **Process Image**: The Azure Function sends the image to the OpenAI deployed model via Azure AI services for classification and analysis.
4. **Store Metadata**: After processing, the image metadata, including classification results, is stored in Azure Cosmos DB.
5. **Consume Data**: The processed metadata can be accessed by front-end applications or other services for further use.



## Project Details
This project aims to demonstrate an efficient and scalable architecture for intelligent image processing using Azure's cloud platform combined with OpenAI's models. It supports a variety of use cases including:
- **E-commerce**: Automatically classifying and tagging product images.
- **Insurance**: Automating claims processing through image recognition.
- **Healthcare**: Analyzing medical images for faster diagnosis.

### UI
The UI for this project allows users to upload images, view classifications, and interact with the processed metadata. Access the UI using the following link: [Project UI](#)

## Setup Instructions
To run this project, follow these steps:
1. Set up your **Azure Blob Storage** to store images.
2. Deploy the **OpenAI model** on Azure AI.
3. Configure **Azure Functions** to handle the image processing pipeline.
4. Set up **Azure Cosmos DB** to store the processed metadata.

## Additional Information
For a detailed overview of the architecture, you can visit the official [Azure Architecture](https://learn.microsoft.com/en-us/azure/architecture/ai-ml/idea/intelligent-apps-image-processing) page.

### Tools and Technologies
- **Azure Blob Storage**: To store images.
- **Azure Event Grid**: To handle events.
- **Azure Functions**: Serverless backend logic.
- **Azure Cosmos DB**: NoSQL database for storing metadata.
- **OpenAI Model**: Deployed on Azure for image classification.



