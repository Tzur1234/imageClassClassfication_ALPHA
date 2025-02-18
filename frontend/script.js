// Event listener for the upload button
document.getElementById('uploadButton').addEventListener('click', handleFileUpload);

// Main function to handle file upload
function handleFileUpload() {
  const fileInput = document.getElementById('imageUpload');
  const file = fileInput.files[0];

  if (!file) {
    alert('Please select an image to upload.');
    return;
  }

  // Validate file and proceed with uploading
  if (isValidFile(file)) {
    const newFilename = generateFilename(file);
    const formData = createFormData(file, newFilename);

    // Show image preview
    showImagePreview(file);

    // Upload the image
    uploadImage(formData, newFilename);
  }
}

// Validates the selected file (type and size)
function isValidFile(file) {
  if (!file.type.startsWith('image/')) {
    alert('Please select a valid image file.');
    return false;
  }

  const maxSize = 5 * 1024 * 1024; // 5 MB
  if (file.size > maxSize) {
    alert('The file is too large. Please select an image under 5MB.');
    return false;
  }

  return true;
}

// Generates a new filename with timestamp and random string
function generateFilename(file) {
  const timestamp = new Date().toISOString().replace(/[:.-]/g, ''); // Removing special characters for filename
  const randomString = Math.random().toString(36).substring(2, 15); // Random string
  const fileExtension = file.name.slice(file.name.lastIndexOf('.')); // Get file extension
  return `${timestamp}-${randomString}${fileExtension}`;
}

// Creates a FormData object to send the file
function createFormData(file, newFilename) {
  const formData = new FormData();
  formData.append('File', file, newFilename); // Append the file with the new name
  return formData;
}

// Displays the image preview before uploading
function showImagePreview(file) {
  const imagePreview = document.getElementById('imagePreview');
  const img = document.createElement('img');
  img.src = URL.createObjectURL(file);
  imagePreview.innerHTML = '';  // Clear previous preview
  imagePreview.appendChild(img);
  imagePreview.style.display = 'block';
}

// Uploads the image to the backend and handles the response
function uploadImage(formData, newFilename) {
  const uploadUrl = 'https://imageclassificationfuncapp31.azurewebsites.net/api/FileUpload?code=SP8X45vdhGE6gTZ2vLwm4Okn2vmP5WTz-vgPEXvWeU9XAzFuM5YY7g==';
  
  fetch(uploadUrl, {
    method: 'POST',
    body: formData
  })
  .then(response => handleUploadResponse(response, newFilename))
  .catch(error => {
    console.error('Error uploading image:', error);
    alert('An error occurred while uploading the image.');
  });
}

// Handles the response after uploading the image
function handleUploadResponse(response, newFilename) {
  if (!response.ok) {
    throw new Error('Failed to upload image. Please try again.');
  }

  response.json()
    .then(data => {
      if (data && data.message === 'file uploaded successfully') {
        console.log('File uploaded successfully');
        pollForAnalysisResult(newFilename);  // Start polling for analysis result
      } else {
        throw new Error('Unexpected response when uploading image.');
      }
    })
    .catch(error => {
      console.error('Error parsing response:', error);
      alert('An error occurred while processing the response.');
    });
}

// Polls for the analysis result using the generated filename
function pollForAnalysisResult(filename) {
  const analysisEndpoint = `https://your-analysis-endpoint.com/api/checkresult?filename=${filename}`; // Replace with your actual endpoint
  let attempts = 0;
  const maxAttempts = 4;
  const interval = 20000; // 20 seconds

  const pollInterval = setInterval(() => {
    if (attempts >= maxAttempts) {
      clearInterval(pollInterval);
      alert('Max attempts reached. Analysis not completed.');
      return;
    }

    fetch(analysisEndpoint)
      .then(response => handleAnalysisResponse(response, pollInterval))
      .catch(error => {
        console.error('Error fetching analysis result:', error);
        attempts++;
        console.log(`Attempt ${attempts} - Error occurred. Retrying...`);
      });
  }, interval);
}

// Handles the response of the polling request for analysis result
function handleAnalysisResponse(response, pollInterval) {
  if (!response.ok) {
    throw new Error('Error fetching analysis result.');
  }

  response.json()
    .then(data => {
      if (data && data.result) {
        // If result found, stop polling
        clearInterval(pollInterval);
        document.getElementById('analysisResult').innerHTML = `Analysis Result: ${data.result}`;
        document.getElementById('analysisResult').style.display = 'block';
      } else {
        // If no result yet, continue polling
        console.log('No result yet. Retrying...');
      }
    })
    .catch(error => {
      console.error('Error parsing analysis response:', error);
    });
}
