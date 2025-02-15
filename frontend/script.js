document.getElementById('uploadButton').addEventListener('click', function() {
    const fileInput = document.getElementById('imageUpload');
    const file = fileInput.files[0];
  
    if (!file) {
      alert('Please select an image to upload.');
      return;
    }
  
    const formData = new FormData();
    formData.append('image', file);
  
    // Show image preview
    const imagePreview = document.getElementById('imagePreview');
    const img = document.createElement('img');
    img.src = URL.createObjectURL(file);
    imagePreview.innerHTML = '';  // Clear previous preview
    imagePreview.appendChild(img);
    imagePreview.style.display = 'block';
  
    // Send image to the backend
    fetch('YOUR_API_ENDPOINT', {  // Replace 'YOUR_API_ENDPOINT' with your actual API endpoint
      method: 'POST',
      body: formData
    })
    .then(response => response.json())
    .then(data => {
      const resultDiv = document.getElementById('analysisResult');
      resultDiv.innerHTML = `Analysis Result: ${data.result}`;  // Adjust the field name based on your response
      resultDiv.style.display = 'block';
    })
    .catch(error => {
      console.error('Error uploading image:', error);
      alert('An error occurred while uploading the image.');
    });
  });
  